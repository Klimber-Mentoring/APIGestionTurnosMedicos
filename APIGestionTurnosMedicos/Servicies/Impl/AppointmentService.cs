using APIGestionTurnosMedicos.Middleware.Exceptions;
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
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private IAppointmentRepository _appointmentRepository { get; set; }
        private IUserRepository _userRepository { get; set; }

        //private IDoctorRepository _doctorRepository { get; set; }

        public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository, /*IDoctorRepository doctorRepository,*/ IUserRepository userRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            // _doctorRepository = doctorRepository;
            _userRepository = userRepository;
        }

        private bool HoraValida(TimeOnly horaInicio)
        {
            if (horaInicio < new TimeOnly(8, 0) || horaInicio > new TimeOnly(18, 0))
                return false;
            return true;
        }
        private bool DiaHabil(DateOnly dia)
        {
            if ((dia.DayOfWeek == DayOfWeek.Saturday) || (dia.DayOfWeek == DayOfWeek.Sunday))
                return false;
            return true;
        }
        private bool DiaFuturo(DateOnly dia, TimeOnly horaInicio)
        {
            var horaActual = TimeOnly.FromDateTime(DateTime.Now);
            var diaActual = DateOnly.FromDateTime(DateTime.Now);

            if (dia == diaActual)
            {
                if (horaInicio > horaActual)
                    return true;
            }

            if (dia > diaActual)
                return true;

            return false;
        }

        private void ValidarDiayHora(DateOnly dia, TimeOnly horaInicio)
        {
            if (!HoraValida(horaInicio))
            {
                throw new BadRequestException("El horario debe estar entre 08:00 y 18:00");
            }

            if (!DiaHabil(dia))
            {
                throw new BadRequestException("El día debe ser hábil");
            }

            if (!DiaFuturo(dia, horaInicio))
            {
                throw new BadRequestException("Solo se pueden generar turnos futuros");
            }

            if (_appointmentRepository.ExisteAppointment(dia, horaInicio))
            {
                throw new BadRequestException("Ya existen turnos cargados en ese horario");
            }

        }


        public AppointmentDTO Create(AppointmentCreateDTO appointmentDTO, string username)
        {
            var paciente = _userRepository.GetByUsername(username);
            if (paciente == null)
            {
                throw new NotFoundException("Paciente no encontrado");
            }

            //var doctor = _doctorRepository.GetById(appointmentDTO.IdDoctor);
            //if (doctor == null)
            //{
            //    throw new NotFoundException("Doctor no encontrado");
            //}
            var doctor = new Doctor();

            ValidarDiayHora(appointmentDTO.Dia, appointmentDTO.HorarioInicio);

            var newAppointment = new Appointment(appointmentDTO.Dia, appointmentDTO.HorarioInicio, paciente, doctor);

            _appointmentRepository.Add(newAppointment);

            return _mapper.Map<AppointmentDTO>(newAppointment);
        }


        public void Delete(Guid id)
        {
            if (id == null)
                throw new BadRequestException("Debe ingresar un id");

            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
                throw new NotFoundException("El turno indicado no se ha encontrado");

            _appointmentRepository.Delete(appointment);

        }

        public List<AppointmentDTO> GetAll()
        {
            var appointments = _appointmentRepository.GetAll();

            List<AppointmentDTO> notasDTO = new List<AppointmentDTO>();

            foreach (Appointment appointment in appointments)
            {
                notasDTO.Add(_mapper.Map<AppointmentDTO>(appointment));
            }
            return (notasDTO.OrderBy(x => x.Dia).ToList());

        }

        public AppointmentDTO GetById(Guid id)
        {
            var appointment = _appointmentRepository.GetById(id);

            if (appointment == null)
                throw new NotFoundException("El turno indicado no se ha encontrado");

            return _mapper.Map<AppointmentDTO>(appointment);
        }



        public AppointmentDTO Update(Guid id, AppointmentUpdateDTO appointmentDTO)
        {
            var appointment = _appointmentRepository.GetById(id);

            //validar nulo
            if (appointment == null)
                throw new NotFoundException("El turno indicado no se ha encontrado");

            if (!DiaValido(appointmentDTO.Dia))
            {
                throw new BadRequestException("El día no es válido");
            }

            if (!_appointmentRepository.ExisteAppointment(appointmentDTO.Dia, appointmentDTO.HorarioInicio, id))
            {
                throw new BadRequestException("Ya existen turnos cargados en ese horario");
            }

            appointment.HorarioInicio = appointmentDTO.HorarioInicio;
            appointment.Dia = appointmentDTO.Dia;

            _appointmentRepository.Update(appointment);

            return _mapper.Map<AppointmentDTO>(appointment);
        }

    }
}
