using APIGestionTurnosMedicos.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace APIGestionTurnosMedicos.Controllers
{
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController
    {
        private IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


    }
}
