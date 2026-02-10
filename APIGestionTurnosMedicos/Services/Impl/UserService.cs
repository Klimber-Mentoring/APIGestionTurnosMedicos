using APIGestionTurnosMedicos.Middleware.Exceptions;
using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;
using APIGestionTurnosMedicos.Models.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace APIGestionTurnosMedicos.Servicies.Impl
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;

        }

        public UserDTO Create(UserCreateDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Username))
                throw new BadRequestException("Debe ingresar un username");

            if (string.IsNullOrWhiteSpace(userDTO.Password))
                throw new BadRequestException("Debe ingresar una contraseña");

            var usuarioEntidad = _mapper.Map<User>(userDTO);

            var hash = new PasswordHasher<User>();
            usuarioEntidad.PasswordHash = hash.HashPassword(usuarioEntidad, userDTO.Password);

            _userRepository.Add(usuarioEntidad);

            return _mapper.Map<UserDTO>(usuarioEntidad);
        }

        public UserDTO GetByUsername(string username)
        {
            var usuarioEntidad = _userRepository.GetByUsername(username);
            if (usuarioEntidad == null)
                throw new NotFoundException("Usuario no encontrado");
            return _mapper.Map<UserDTO>(usuarioEntidad);
        }

        public bool ValidarPassword(UserDTO userDTO, string passwordIngresado)
        {
            var usuario = _userRepository.GetByUsername(userDTO.Username);
            var hash = new PasswordHasher<User>();
            var passwordValida = hash.VerifyHashedPassword(usuario, usuario.PasswordHash, passwordIngresado);
            return (passwordValida == PasswordVerificationResult.Success);
        }
    }
}
