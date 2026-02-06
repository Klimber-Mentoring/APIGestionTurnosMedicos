using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Servicies.Impl
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private IUserRepository _userRepository {  get; set; }

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public UserDTO Create(UserCreateDTO userDTO)
        {
            var usuarioEntidad = _mapper.Map<User>(userDTO);

            var hash = new PasswordHasher<User>();
            usuarioEntidad.PasswordHash = hash.HashPassword(usuarioEntidad, userDTO.Password);

            _userRepository.Add(usuarioEntidad);

            return _mapper.Map<UserDTO>(usuarioEntidad);
        }
    }
}
