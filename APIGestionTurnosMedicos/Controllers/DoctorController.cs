using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace APIGestionTurnosMedicos.Controllers
{
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : ControllerBase  
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Carga un nuevo doctor.
        /// </summary>
        /// <param name="doctor">Datos del doctor que se quiere registrar</param>
        /// <returns>Doctor registrado</returns>
        /// <response code="201">El doctor fue registrado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorDTO doctor)
        {
            var createdDoctor = _doctorService.CreateDoctor(doctor);
            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.Id }, createdDoctor); 
        }


        /// <summary>
        /// Devuelve todos los doctores registrados.
        /// </summary>
        /// <returns>Lista de doctores</returns>
        /// <response code="200">Devuelve lista de doctores</response>
        
        [HttpGet]
        public IActionResult GetDoctores()
        {
            var doctores = _doctorService.GetDoctores();
            return Ok(doctores);  // 200 OK
        }


        /// <summary>
        /// Devuelve un doctor en base a su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de un doctor</param>
        /// <returns>Datos del doctor solicitado</returns>
        /// <response code="200">Devuelve el doctor solicitado</response>
        /// <response code="404">No se encontró un doctor con el id correspondiente</response> 

        [HttpGet("{id}")]
        public IActionResult GetDoctor(Guid id)
        {
            var doctor = _doctorService.GetDoctor(id);
            return Ok(doctor);  // 200 OK
        }


        /// <summary>
        /// Actualiza datos de un doctor registrado previamente.
        /// </summary>
        /// <param name="id">Identificador del doctor a actualizar</param>
        /// <param name="doctor">Datos actualizados del doctor</param>
        /// <response code="204">Los datos del doctor se actualizaron correctamente</response>
        /// <response code="404">No se encontró un doctor con el id correspondiente</response>

        [HttpPut("{id}")]  
        public IActionResult UpdateDoctor(Guid id, [FromBody] UpdateDoctorDTO doctor)
        {
            var updatedDoctor = _doctorService.UpdateDoctor(id, doctor);
            return Ok(updatedDoctor);  // 200 OK
        }


        /// <summary>
        /// Elimina un doctor.
        /// </summary>
        /// <param name="id">Identificador del doctor que se quiere eliminar</param>
        /// <response code="204">El doctor se eliminó correctamente</response>
        /// <response code="404">No se encontró un doctor con el id correspondiente</response>

        [HttpDelete("{id}")]  
        public IActionResult DeleteDoctor(Guid id)
        {
            _doctorService.DeleteDoctor(id);
            return NoContent();  // 204 No Content 
        }
    }
}