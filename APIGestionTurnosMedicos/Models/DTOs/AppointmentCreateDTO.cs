using System.Numerics;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class AppointmentCreateDTO
    {
        public DateOnly Dia { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public Guid IdDoctor { get; set; }

        public AppointmentCreateDTO(DateOnly dia, TimeOnly horarioInicio, Guid idDoctor)
        {
            Dia = dia;
            HorarioInicio = horarioInicio;
            IdDoctor = idDoctor;
        }
    }
}
