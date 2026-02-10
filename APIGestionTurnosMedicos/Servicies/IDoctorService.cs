using APIGestionTurnosMedicos.Models.DTOs;

namespace APIGestionTurnosMedicos.Servicies
{
    public interface IDoctorService
    {
        List<DoctorDTO> GetDoctores();
        DoctorDTO GetDoctor(Guid id);
        DoctorDTO CreateDoctor(DoctorDTO doctor);
        DoctorDTO UpdateDoctor(Guid id, UpdateDoctorDTO doctor);
        void DeleteDoctor(Guid id);
    }
}
