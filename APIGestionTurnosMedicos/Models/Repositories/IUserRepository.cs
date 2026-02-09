using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByUsername(string username);

    }
}
