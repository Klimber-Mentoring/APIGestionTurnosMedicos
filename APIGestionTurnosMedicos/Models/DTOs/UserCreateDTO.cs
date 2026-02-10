using APIGestionTurnosMedicos.Enums;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public UserCreateDTO(string username, string password, string rol)
        {
            Username = username;
            Password = password;
            Rol = rol;
        }

    }
}
