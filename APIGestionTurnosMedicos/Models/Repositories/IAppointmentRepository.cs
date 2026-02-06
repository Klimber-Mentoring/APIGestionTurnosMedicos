using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        List<Appointment> GetAll();
    }
}
