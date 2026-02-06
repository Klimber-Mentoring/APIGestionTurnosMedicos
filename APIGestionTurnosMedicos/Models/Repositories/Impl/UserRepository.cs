using APIGestionTurnosMedicos.Models.Entities;

namespace APIGestionTurnosMedicos.Models.Repositories.Impl
{
    public class UserRepository: IUserRepository
    {
        public List<User> Users { get; set; }

        public UserRepository()
        {
            Users = new List<User>();
        }

        public void Add(User user)
        {
            Users.Add(user);
        }

        public User GetById(Guid id)
        {
            foreach (var user in Users)
            {
                if (user.Id == id)
                    return user;
            }
            return null;
        }
    }
}