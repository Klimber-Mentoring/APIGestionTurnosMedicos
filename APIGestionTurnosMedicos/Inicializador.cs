using APIGestionTurnosMedicos.Enums;
using APIGestionTurnosMedicos.Servicies;
using Microsoft.AspNetCore.Identity;
using APIGestionTurnosMedicos.Models.DTOs;

namespace APIGestionTurnosMedicos
{
    public class Inicializador
    {
        private readonly IUserService _userService;

        public Inicializador(IUserService userService)
        {
            _userService = userService;
        }

        public void CargarDatosPrueba()
        {
            _userService.Create(new UserCreateDTO("admin1", "admin123", "Admin"));
            _userService.Create(new UserCreateDTO("user1", "user123", "User"));
        }
    }
}
