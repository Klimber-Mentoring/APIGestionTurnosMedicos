using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        List<Appointment> GetAll();
        Appointment Update(Appointment updatedAppointment);
        void Delete(Appointment appointment);
        Appointment GetById(Guid id);
        bool ExisteAppointment(DateOnly dia, TimeOnly horarioInicio, Guid? idExcluido = null);

    }
}
