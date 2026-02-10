using APIGestionTurnosMedicos.Enums;
using AutoMapper;

namespace APIGestionTurnosMedicos.Helpers
{
    public class UserToStringConverter : ITypeConverter<UserRole, string>
    {
        public string Convert(UserRole source, string destination, ResolutionContext context)
        {
            return source switch
            {
                UserRole.ADMIN => "Admin",
                UserRole.USER => "User"
            };
        }
    }

}
