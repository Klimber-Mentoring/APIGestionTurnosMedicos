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
        /// Devuelve todos los turnos realizados.
        /// </summary>
        /// <returns>Lista de turnos realizados</returns>
        /// <response code="200">Devuelve la lista de turnos</response>

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
        /// <response code="404">No se encontró un turno con el id correspondiente</response> 

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AppointmentDTO> Get(Guid id)
        {
            return _appointmentService.GetById(id);
        }

        /// <summary>
        /// Crea un nuevo turno.
        /// </summary>
        /// <param name="nota">Datos del turno que se quiere reservar</param>
        /// <returns>Turno creado</returns>
        /// <response code="201">El turno fue creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        /// 
        [Authorize(Roles = "User")]

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(AppointmentCreateDTO appointment)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newAppointment = _appointmentService.Create(appointment, username);

            return Created("", new { id = newAppointment.Id });
        }


        /// <summary>
        /// Actualiza un turno existente.
        /// </summary>
        /// <param name="id">Identificador del turno a actualizar</param>
        /// <param name="nota">Datos actualizados del turno</param>
        /// <response code="204">El turno se actualizó correctamente</response>
        /// <response code="404">No se encontró el turno a actualizar</response>

        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AppointmentDTO> Update(Guid id, AppointmentUpdateDTO appointment)
        {
            var updatedAppointment = _appointmentService.Update(Guid id, appointment);

            return updatedAppointment;
        }

        /// <summary>
        /// Elimina un turno.
        /// </summary>
        /// <param name="id">Identificador del turno a eliminar</param>
        /// <response code="204">El turno se eliminó correctamente</response>
        /// <response code="404">No se encontró el turno a eliminar</response>
        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            _appointmentService.Delete(id);

            return NoContent();
        }

    }
}
