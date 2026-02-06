using APIGestionTurnosMedicos.Enums;
using Microsoft.AspNetCore.Identity;

namespace APIGestionTurnosMedicos.Models.Entities
{
    public class User: IdentityUser<Guid>
    {
        public UserRole Rol { get; set; }
        
        public User(UserRole rol)
        {
            Rol = rol;               
        }

    }
}
