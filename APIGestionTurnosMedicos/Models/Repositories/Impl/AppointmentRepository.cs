using APIGestionTurnosMedicos.Middleware.Exceptions;
using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories.Impl
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private List<Appointment> Appointments { get; set; }

        public AppointmentRepository()
        {
            Appointments = new List<Appointment>();
        }

        public void Add(Appointment appointment)
        {
            Appointments.Add(appointment);
        }

        public List<Appointment> GetAll()
        {
            return Appointments;
        }

        public Appointment Update(Appointment updatedAppointment)
        {
            var appointment = GetById(updatedAppointment.Id);

            if (appointment != null)
            {
                appointment.Dia = updatedAppointment.Dia;
                appointment.HorarioInicio = updatedAppointment.HorarioInicio;
            }

            return null;
        }

        public void Delete(Appointment appointment)
        {
            Appointments.Remove(appointment);
        }

        public Appointment GetById(Guid id)
        {
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.Id == id)
                {
                    return appointment;
                }
            }
            return null;
        }

        public bool ExisteAppointment(DateOnly dia, TimeOnly horarioInicio, Guid? idExcluido = null)
        {
            foreach (var appointment in Appointments)
            {
                if (idExcluido.HasValue && idExcluido == appointment.Id)
                    continue;

                if (appointment.DiaRepite(dia))
                {
                    return appointment.HoraOcupada(horarioInicio);
                }
            }
            return false;
        }

    }
}
