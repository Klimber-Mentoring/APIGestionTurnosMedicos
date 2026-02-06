using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories.Impl
{
    public class AppointmentRepository: IAppointmentRepository
    {
        public List<Appointment> Appointments { get; set; }

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

    }
}
