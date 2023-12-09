using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PLL.Models;
using System.Diagnostics;
using Bogus;
using MongoDB.Bson;
using MongoDB.Driver;
using PLL.Data.Dao;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.MongoDbDao;
using PLL.Data.Entity;
using PLL.Data.Infastracture;


namespace PLL.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDaoAccessor _accessor;

        public HomeController(ILogger<HomeController> logger,IDaoAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var listRoles = new List<Role>
            {
                new Role
                {
                    RoleName = "Admin"
                },
                new Role
                {
                    RoleName = "User"
                }
            };

            foreach (var role in listRoles)
            {
                _accessor.RoleDao.CreateAsync(role);
            }

            var faker = new Faker<User>()
                .RuleSet("default", rule =>
                {
                    rule.RuleFor(u => u.Login, f => f.Internet.UserName());
                    rule.RuleFor(u => u.Password, f => f.Internet.Password());
                    rule.RuleFor(u => u.Email, f => f.Internet.Email());
                    rule.RuleFor(u => u.Age, f => f.Random.Number(17, 67));
                });

            var fakeUsers = faker.Generate(20, "default");

            return View();
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
