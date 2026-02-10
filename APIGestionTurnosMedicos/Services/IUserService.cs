using APIGestionTurnosMedicos.Models.DTOs;
using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Servicies
{
    public interface IUserService
    {
        UserDTO Create(UserCreateDTO userDTO);
        UserDTO GetByUsername(string username);

        bool ValidarPassword(UserDTO usuario, string passwordIngresado);
    }
}
