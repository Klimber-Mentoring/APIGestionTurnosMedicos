using APIGestionTurnosMedicos.Enums;
using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;
using APIGestionTurnosMedicos.Servicies;
using Microsoft.AspNetCore.Identity;

namespace APIGestionTurnosMedicos
{
    public class Inicializador
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        public Inicializador(IUserService userService, IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public void CargarDatosPrueba()
        {
            Guid doctor1ID = Guid.Parse("6a1b2c3d-4e5f-6789-0123-456789abcdef");
            Guid doctor2ID = Guid.Parse("5a1b2c3d-4e5f-6789-0123-456789abcdef");
            Guid doctor3ID = Guid.Parse("4a1b2c3d-4e5f-6789-0123-456789abcdef");

            _userService.Create(new UserCreateDTO("admin1", "admin123", "Admin"));
            _userService.Create(new UserCreateDTO("user1", "user123", "User"));

            _doctorService.CreateDoctor(new DoctorDTO(doctor1ID, "René", "Favaloro", "17806253", "CARDIOLOGIA"));
            _doctorService.CreateDoctor(new DoctorDTO(doctor2ID, "Cecilia", "Grierson", "22123654", "GINECOLOGIA"));
            _doctorService.CreateDoctor(new DoctorDTO(doctor3ID, "Facundo", "Cosme", "12345678", "CIRUGIA"));

        }
    }
}
