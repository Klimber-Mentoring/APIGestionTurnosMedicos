using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;
using APIGestionTurnosMedicos.Enums;
using AutoMapper;

namespace APIGestionTurnosMedicos.Helpers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => src.Especialidad.ToString()));

            CreateMap<DoctorDTO, Doctor>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => Enum.Parse<Especialidad>(src.Especialidad, true)));

            CreateMap<Doctor, UpdateDoctorDTO>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => src.Especialidad.ToString()));

            CreateMap<UpdateDoctorDTO, Doctor>()
                .ForMember(dest => dest.Especialidad,
                    opt => opt.MapFrom(src => Enum.Parse<Especialidad>(src.Especialidad, true)));
        }
    }
}