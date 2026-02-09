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

        [HttpGet]
        public IActionResult GetDoctores()
        {
            var doctores = _doctorService.GetDoctores();
            return Ok(doctores);  // 200 OK
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctor(Guid id)
        {
            var doctor = _doctorService.GetDoctor(id);
            return Ok(doctor);  // 200 OK
        }

        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorDTO doctor)
        {
            var createdDoctor = _doctorService.CreateDoctor(doctor);
            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.Id }, createdDoctor);  // 201 Created
        }

        [HttpPut("{id}")]  
        public IActionResult UpdateDoctor(Guid id, [FromBody] UpdateDoctorDTO doctor)
        {
            var updatedDoctor = _doctorService.UpdateDoctor(id, doctor);
            return Ok(updatedDoctor);  // 200 OK
        }

        [HttpDelete("{id}")]  
        public IActionResult DeleteDoctor(Guid id)
        {
            _doctorService.DeleteDoctor(id);
            return NoContent();  // 204 No Content 
        }
    }
}