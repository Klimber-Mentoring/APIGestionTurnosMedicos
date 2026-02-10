using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace APIGestionTurnosMedicos.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        public AppointmentController(IUserService userService, IAppointmentService appointmentService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Carga un nuevo turno.
        /// </summary>
        /// <param name="appointment">Datos del turno que se quiere reservar</param>
        /// <returns>Turno creado</returns>
        /// <response code="201">El turno fue creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">No tiene los permisos para realizar esta solicitud</response>

        [Authorize(Roles = ("Admin, User"))]
        [HttpPost()]

        public IActionResult Create(AppointmentCreateDTO appointment)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newAppointment = _appointmentService.Create(appointment, username);

            return Created("", new { id = newAppointment.Id });
        }

        /// <summary>
        /// Devuelve todos los turnos realizados.
        /// </summary>
        /// <returns>Lista de turnos realizados</returns>
        /// <response code="200">Devuelve la lista de turnos</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">No tiene los permisos para realizar esta solicitud</response>

        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public ActionResult<List<AppointmentDTO>> GetAll()
        {
            return _appointmentService.GetAll();

        }

        /// <summary>
        /// Devuelve un turno específico en base a su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de un turno</param>
        /// <returns>El turno solicitado</returns>
        /// <response code="200">Devuelve el turno encontrado</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="404">No se encontró un turno con el id correspondiente</response> 
        /// <response code="403">No tiene los permisos para realizar esta solicitud</response>

        [HttpGet("{id}")]

        public ActionResult<AppointmentDTO> Get(Guid id)
        {
            return _appointmentService.GetById(id);
        }

        /// <summary>
        /// Actualiza un turno existente.
        /// </summary>
        /// <param name="id">Identificador del turno a actualizar</param>
        /// <param name="appointment">Datos actualizados del turno</param>
        /// <response code="204">El turno se actualizó correctamente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="404">No se encontró el turno a actualizar</response>
        /// <response code="403">No tiene los permisos para realizar esta solicitud</response>

        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]

        public ActionResult<AppointmentDTO> Update(Guid id, AppointmentUpdateDTO appointment)
        {
            var updatedAppointment = _appointmentService.Update(id, appointment);

            return updatedAppointment;
        }

        /// <summary>
        /// Elimina un turno.
        /// </summary>
        /// <param name="id">Identificador del turno a eliminar</param>
        /// <response code="204">El turno se eliminó correctamente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="404">No se encontró un turno con el id correspondiente</response>
        /// <response code="403">No tiene los permisos para realizar esta solicitud</response>

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            _appointmentService.Delete(id);

            return NoContent();
        }

    }
}
