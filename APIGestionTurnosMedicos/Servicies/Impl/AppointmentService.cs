using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;
using APIGestionTurnosMedicos.Models.Repositories;
using APIGestionTurnosMedicos.Models.Repositories.Impl;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace APIGestionTurnosMedicos.Servicies.Impl
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IMapper _mapper;
        private IAppointmentRepository _appointmentRepository { get; set; }
        private IUserRepository _userRepository { get; set; }

        public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;

        }


        private bool DiaValido(DateOnly dia)
        {
            if ((dia.DayOfWeek == DayOfWeek.Saturday) || (dia.DayOfWeek == DayOfWeek.Sunday))
                return false;
            return true;
        }

        private bool DiaDisponible(DateOnly dia, TimeOnly horarioInicio)
        {
            var appointments = _appointmentRepository.GetAll();
            foreach(var appointment in appointments)
            {
                if (appointment.DiaRepite(dia))
                {
                    return appointment.HoraDisponible(horarioInicio);
                }
            }
            return false;
        }


        public AppointmentDTO Create(AppointmentCreateDTO appointmentDTO, Guid userId)
        {
            var paciente = _userRepository.GetById(userId);
            if (paciente == null)
            {
                throw new Exception("Paciente no encontrado");
            }

            var doctor = _doctorRepository.GetById(appointmentDTO.IdDoctor);
            if (doctor == null)
            {
                throw new Exception("Doctor no encontrado");
            }

            if (!DiaValido(appointmentDTO.Dia))
            {
                throw new Exception("El día no es válido");
            }

            if (!DiaDisponible(appointmentDTO.Dia, appointmentDTO.HorarioInicio))
            {
                throw new Exception("Ya existen turnos cargados en ese horario");
            }

            var newAppointment = _mapper.Map<Appointment>(appointmentDTO);
            _appointmentRepository.Add(newAppointment);

            return _mapper.Map<AppointmentDTO>(newAppointment);
        }


        public void Delete(AppointmentDTO AppointmentDTO)
        {
            // validar turno existente
            // delete
        }

        public List<AppointmentDTO> GetAll()
        {

        }

        public AppointmentDTO GetById(Guid id)
        {

        }

    }
}
