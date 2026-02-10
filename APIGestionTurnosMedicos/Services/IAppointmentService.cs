using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Servicies
{
    public interface IAppointmentService
    {
        AppointmentDTO Create(AppointmentCreateDTO appointmentDTO, string username);
        void Delete(Guid id);
        List<AppointmentDTO> GetAll();
        AppointmentDTO GetById(Guid id);
        AppointmentDTO Update(Guid id, AppointmentUpdateDTO appointmentDTO);
    }
}
