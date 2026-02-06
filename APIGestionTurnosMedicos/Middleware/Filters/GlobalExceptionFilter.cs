using APIGestionTurnosMedicos.Middleware.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// Basicamente, cree una clase BaseException que hereda de Exception y tiene una propiedad StatusCode para almacenar el código de estado HTTP asociado a la excepción.
// Sobre esta clase cree las excepciones personalizadas que tienen un codigo http propio. En este filtro (cuando yo disparo una excepción desde el service) valido 
// si la excepcion es una de las personalizadas o si no está controlada (le devuelvo un 500 al usuario con un mensaje genérico y me imprimo por consola el error)

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        int statusCode;
        string message;

        if (context.Exception is BaseException customException)
        {
            statusCode = customException.StatusCode;
            message = customException.Message;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            message = "Ocurrió un error inesperado en el servidor.";

            _logger.LogError(context.Exception, "ERROR NO CONTROLADO: {Message}", context.Exception.Message);
        }

        context.Result = new ObjectResult(new { error = message })
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}