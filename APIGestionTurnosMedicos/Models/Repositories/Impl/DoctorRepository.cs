using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly List<Doctor> _doctores;

        public DoctorRepository()
        {
            _doctores = new List<Doctor>();
        }

        public List<Doctor> GetDoctores()
        {
            return _doctores;
        }

        public Doctor GetDoctor(Guid id)
        {
            return _doctores.FirstOrDefault(d => d.Id == id);
        }

        public Doctor CreateDoctor(Doctor doctor)
        {
            doctor.Id = Guid.NewGuid();
            _doctores.Add(doctor);
            return doctor;
        }

        public Doctor UpdateDoctor(Doctor doctor)
        {
            var index = _doctores.FindIndex(d => d.Id == doctor.Id);
            if (index != -1)
            {
                _doctores[index] = doctor;
            }
            return doctor;
        }

        public void DeleteDoctor(Guid id)
        {
            var doctor = _doctores.FirstOrDefault(d => d.Id == id);
            if (doctor != null)
            {
                _doctores.Remove(doctor);
            }
        }
    }
}
