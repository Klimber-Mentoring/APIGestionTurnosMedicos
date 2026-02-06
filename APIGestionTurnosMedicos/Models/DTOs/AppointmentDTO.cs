using APIGestionTurnosMedicos.Models.Entities;
using System.Numerics;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public DateOnly Dia { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFin { get; set; }

        public User Paciente { get; set; }
        public Doctor Doctor { get; set; }
    }
}
