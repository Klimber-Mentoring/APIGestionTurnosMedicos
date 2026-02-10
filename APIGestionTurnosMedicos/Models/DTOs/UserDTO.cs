using APIGestionTurnosMedicos.Enums;

namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Rol { get; set; }

        public UserDTO(string username, string rol)
        {
            Username = username;
            Rol = rol;
        }
    }
}