using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories
{
    public interface IDoctorRepository
    {
        List<Doctor> GetDoctores();
        Doctor GetDoctor(Guid id);
        Doctor CreateDoctor(Doctor doctor);
        Doctor UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Guid id);
    }
}
