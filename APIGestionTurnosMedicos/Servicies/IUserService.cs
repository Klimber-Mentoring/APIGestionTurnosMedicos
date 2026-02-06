using APIGestionTurnosMedicos.Models.DTOs;

namespace APIGestionTurnosMedicos.Servicies
{
    public interface IUserService
    {
        UserDTO Create(UserCreateDTO userDTO);
    }
}
