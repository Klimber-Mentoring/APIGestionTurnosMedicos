using APIGestionTurnosMedicos.Servicies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace APIGestionTurnosMedicos.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IUserService _userService;
        public BasicAuthenticationHandler(IUserService userService, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unathorized");
            }

            if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = authorizationHeader.Substring(6);

            var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var credentials = credentialAsString.Split(":");
            if (credentials?.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var username = credentials[0];
            var password = credentials[1];            

            var usuario = _userService.GetByUsername(username);

            //repo
            if ((usuario == null) || !_userService.ValidarPassword(usuario, password))
            {
                return AuthenticateResult.Fail("Authentication failed");
            }


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())  //repo
            }; //List of attributes for the current user


            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }


    }
}
