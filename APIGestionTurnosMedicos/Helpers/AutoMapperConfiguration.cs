using APIGestionTurnosMedicos.Enums;
using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;
using AutoMapper;

namespace APIGestionTurnosMedicos.Helpers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<UserRole, string>().ConvertUsing<UserToStringConverter>();
            CreateMap<string, UserRole>().ConvertUsing<StringToUserConverter>();
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => src.Especialidad.ToString()));


            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<DoctorDTO, Doctor>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => Enum.Parse<Especialidad>(src.Especialidad, true)));

            CreateMap<Doctor, UpdateDoctorDTO>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => src.Especialidad.ToString()));

            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentCreateDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentUpdateDTO>().ReverseMap();
            CreateMap<UpdateDoctorDTO, Doctor>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => Enum.Parse<Especialidad>(src.Especialidad, true)));
        }

    }
}