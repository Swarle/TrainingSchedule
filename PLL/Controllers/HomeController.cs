using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PLL.Models;
using System.Diagnostics;
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
        //[HttpGet("test")]
        //public async Task<ActionResult> Test()
        //{

        //}

        //public async Task<ActionResult> CheckConStatus()
        //{

        //}
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
