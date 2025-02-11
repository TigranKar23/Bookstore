using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bookstore.BLL.Services.UserService;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Bookstore.DAL.Models;

namespace Bookstore.BLL.Helpers
{
    public class UserAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public UserAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                         ILoggerFactory logger,
                                         UrlEncoder encoder,
                                         ISystemClock clock,
                                         IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string token = null;

            token = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Authorization not provided");
            }

            // var user = await _userService.GetUserByIdAsync(token);
            User user = null;

            if (user == null)
            {
                return AuthenticateResult.Fail("User Not Found");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
