using APIGestionTurnosMedicos.Enums;
using APIGestionTurnosMedicos.Middleware.Exceptions;
using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;
using APIGestionTurnosMedicos.Models.Repositories;
using AutoMapper;

namespace APIGestionTurnosMedicos.Servicies.Impl
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(
            IDoctorRepository doctorRepository,
            IMapper mapper,
            ILogger<DoctorService> logger)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public DoctorDTO CreateDoctor(DoctorDTO doctor)
        {
            ValidateDoctorData(doctor.Nombre, doctor.Apellido, doctor.Dni, doctor.Especialidad);

            Doctor doctorEntidad = _mapper.Map<Doctor>(doctor);

            Doctor doctorCreado = _doctorRepository.CreateDoctor(doctorEntidad);

            _logger.LogInformation("Doctor creado exitosamente con ID: {DoctorId}", doctorCreado.Id);

            return _mapper.Map<DoctorDTO>(doctorCreado);
        }

        public void DeleteDoctor(Guid id)
        { 
            if (id == Guid.Empty)
            {
                throw new BadRequestException("El ID del doctor no puede estar vacío");
            }

            var doctor = _doctorRepository.GetDoctor(id);
            if (doctor == null)
            {
                _logger.LogWarning("Intento de eliminar doctor inexistente con ID: {DoctorId}", id);
                throw new NotFoundException($"No se encontró un doctor con el ID {id}");
            }

            _doctorRepository.DeleteDoctor(id);

            _logger.LogInformation("Doctor eliminado exitosamente. ID: {DoctorId}", id);
        }

        public DoctorDTO GetDoctor(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadRequestException("El ID del doctor no puede estar vacío");
            }

            Doctor doctor = _doctorRepository.GetDoctor(id);

            if (doctor == null)
            {
                _logger.LogWarning("Doctor no encontrado con ID: {DoctorId}", id);
                throw new NotFoundException($"No se encontró un doctor con el ID {id}");
            }

            return _mapper.Map<DoctorDTO>(doctor);
        }

        public List<DoctorDTO> GetDoctores()
        {
            List<Doctor> doctores = _doctorRepository.GetDoctores();

            _logger.LogInformation("Se obtuvieron {Count} doctores", doctores.Count);

            return _mapper.Map<List<DoctorDTO>>(doctores);
        }

        public DoctorDTO UpdateDoctor(Guid id, UpdateDoctorDTO doctor)
        {
            if (id == Guid.Empty)
            {
                throw new BadRequestException("El ID del doctor no puede estar vacío");
            }

            var existingDoctor = _doctorRepository.GetDoctor(id);
            if (existingDoctor == null)
            {
                _logger.LogWarning("Intento de actualizar doctor inexistente con ID: {DoctorId}", id);
                throw new NotFoundException($"No se encontró un doctor con el ID {id}");
            }

            ValidateDoctorData(doctor.Nombre, doctor.Apellido, doctor.Dni, doctor.Especialidad);

            Doctor doctorEntidad = _mapper.Map<Doctor>(doctor);
            doctorEntidad.Id = id; 

            Doctor doctorActualizado = _doctorRepository.UpdateDoctor(doctorEntidad);

            _logger.LogInformation("Doctor actualizado exitosamente. ID: {DoctorId}", id);

            return _mapper.Map<DoctorDTO>(doctorActualizado);
        }

        // Método privado para validar datos del doctor
        private void ValidateDoctorData(string nombre, string apellido, string dni, string especialidad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new BadRequestException("El nombre del doctor es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new BadRequestException("El apellido del doctor es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new BadRequestException("El DNI del doctor es obligatorio");
            }

            ValidarDni(dni);

            if (string.IsNullOrWhiteSpace(especialidad))
            {
                throw new BadRequestException("La especialidad es obligatoria");
            }

            if (!Enum.TryParse<Especialidad>(especialidad, true, out _))
            {
                var especialidadesValidas = string.Join(", ", Enum.GetNames(typeof(Especialidad)));
                throw new BadRequestException($"La especialidad '{especialidad}' no es válida. Especialidades disponibles: {especialidadesValidas}");
            }
        }

        private void ValidarDni(string dni)
        {
            string dniLimpio = dni.Replace(".", "").Trim();

            if (dniLimpio.Length < 7 || dniLimpio.Length > 8)
            {
                throw new BadRequestException("DNI inválido. Debe tener entre 7 y 8 dígitos");
            }
            foreach (char c in dniLimpio)
            {
                if (!char.IsDigit(c))
                {
                    throw new BadRequestException("DNI inválido. Debe contener solo números");
                }
            }
        }
    }
}