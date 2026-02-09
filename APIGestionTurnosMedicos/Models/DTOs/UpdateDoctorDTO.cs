using APIGestionTurnosMedicos.Enums;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class UpdateDoctorDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public String Especialidad { get; set; }
    }
}
