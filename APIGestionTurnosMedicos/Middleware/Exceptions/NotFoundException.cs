namespace APIGestionTurnosMedicos.Middleware.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, StatusCodes.Status404NotFound) { }
    }
}
