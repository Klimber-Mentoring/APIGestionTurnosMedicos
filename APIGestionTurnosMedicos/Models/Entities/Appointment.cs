namespace APIGestionTurnosMedicos.Models.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateOnly Dia { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFin { get; set; }

        public User Paciente { get; set; }
        public Doctor Doctor { get; set; }

        public Appointment(DateOnly dia, TimeOnly horarioInicio, User paciente, Doctor doctor)
        {
            Id = Guid.NewGuid();
            HorarioInicio = horarioInicio;
            HorarioFin = horarioInicio.AddHours(1);
            Paciente = paciente;
            Doctor = doctor;
        }

        public bool DiaRepite(DateOnly dia)
        {
            return (Dia == dia);
        }

        public bool HoraOcupada(TimeOnly inicio)
        {
            var fin = inicio.AddHours(1);
            return ((inicio.IsBetween(HorarioInicio, HorarioFin)) || (fin.IsBetween(HorarioInicio, HorarioFin)));
        }
        
    }

}
