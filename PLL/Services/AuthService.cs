using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Specification;
using PLL.Infostracture;
using PLL.Services.Interfaces;

namespace PLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpContext _context;
        private readonly IDaoAccessor _daoAccessor;

        public AuthService(IHttpContextAccessor accessor, IDaoAccessor daoAccessor)
        {
            _context = accessor.HttpContext;
            _daoAccessor = daoAccessor;
        }

        public async Task RegisterAsync(string login, string password, string roleName)
        {
            var userSpecification = new GetUserByLoginSpecification(login);

            var checkUser = await _daoAccessor.UserDao.FindSingle(userSpecification);

            if (checkUser != null)
            {
                throw new HttpException($"User with login {login} already exist",HttpStatusCode.BadRequest);
            }

            var roleSpecification = new GetRoleByRoleNameSpecification(roleName);

            var role = await _daoAccessor.RoleDao.FindSingle(roleSpecification);

            if (role == null)
            {
                throw new HttpException("Role with that name don't exist",HttpStatusCode.NotFound);
            }

            var user = new User
            {
                Login = login,
                Password = password,
                RoleId = role.Id
            };

            await _daoAccessor.UserDao.CreateAsync(user);

            var claims = new List<Claim>
                { new Claim(ClaimTypes.Name, user.Login), new Claim(ClaimTypes.Role, roleName) };
            var claimsIdentity = new ClaimsIdentity(claims,"Cookies");

            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public async Task LoginAsync(string login, string password)
        {
            var userSpecification = new GetUserByLoginSpecification(login);

            var user = await _daoAccessor.UserDao.FindSingle(userSpecification);

            if (user == null)
            {
                throw new HttpException("User don't exist", HttpStatusCode.NotFound);
            }

            if (user.Password != password)
            {
                throw new HttpException("Password don't match",HttpStatusCode.Unauthorized);
            }

            var role = await _daoAccessor.RoleDao.GetByIdAsync(user.RoleId);

            var claims = new List<Claim>
                { new Claim(ClaimTypes.Name, user.Login), new Claim(ClaimTypes.Role, role.RoleName) };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
