using System.Numerics;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class AppointmentCreateDTO
    {
        public DateOnly Dia { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public Guid IdDoctor { get; set; }
    }
}
