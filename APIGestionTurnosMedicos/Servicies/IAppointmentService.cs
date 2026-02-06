using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Servicies
{
    public interface IAppointmentService
    {
        AppointmentDTO Create(AppointmentCreateDTO appointmentDTO, Guid userId);
        void Delete(AppointmentDTO AppointmentDTO);
        List<AppointmentDTO> GetAll();
        AppointmentDTO GetById(Guid id);
    }
}
