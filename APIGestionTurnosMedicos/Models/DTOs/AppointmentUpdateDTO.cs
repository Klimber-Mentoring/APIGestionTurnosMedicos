namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class AppointmentUpdateDTO
    {
        public DateOnly Dia { get; set; }
        public TimeOnly HorarioInicio { get; set; }
    }
}
