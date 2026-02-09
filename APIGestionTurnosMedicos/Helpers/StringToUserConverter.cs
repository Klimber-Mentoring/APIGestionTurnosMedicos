using APIGestionTurnosMedicos.Enums;
using AutoMapper;

namespace APIGestionTurnosMedicos.Helpers
{
    public class StringToUserConverter : ITypeConverter<string, UserRole>
    {
        public UserRole Convert(string source, UserRole destination, ResolutionContext context)
        {
            return source.Trim().ToUpper() switch
            {
                "ADMIN" => UserRole.ADMIN,
                "USER" => UserRole.USER
            };
        }
    }
}
