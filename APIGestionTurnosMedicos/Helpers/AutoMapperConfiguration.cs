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


            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();


            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentCreateDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentUpdateDTO>().ReverseMap();
        }

    }
}
