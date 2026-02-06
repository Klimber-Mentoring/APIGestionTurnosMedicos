using APIGestionTurnosMedicos.Enums;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public UserRole Rol { get; set; }

        public UserDTO(string username, UserRole rol)
        {
            Username = username;
            Rol = rol;
        }
    }
}