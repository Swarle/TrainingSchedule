using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDaoAccessor _accessor;

        public HomeController(ILogger<HomeController> logger,IDaoAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<ActionResult> Test()
        {
            //var client = new MongoClient("mongodb://localhost:27017");

            //var db = client.GetDatabase("TrainingSchedule");

            //var collection = db.GetCollection<BsonDocument>("Set");

            //var docs = collection.Find(new BsonDocument()).ToList();

            //var set = new Set();

            //foreach (var doc in docs)
            //{
            //    set = new Set
            //    {
            //        Id = doc["_id"].AsObjectId.ToString(),
            //        NumberRepetitons = doc["number_repetitions"].AsInt32,
            //        ExerciseId = doc["exercise_id"].AsObjectId.ToString(),
            //        Weight = doc["weight"].AsInt32,
            //        Unit = new Unit
            //        {
            //            UnitName = doc["unit"].AsString
            //        }
            //    };
            //}

            var dao = new TrainingDao(_logger);

            var entity = await dao.GetByIdAsync("6557650ef425a75f5f410183");

            entity.Date = DateTime.Today.AddYears(1);

            await dao.UpdateAsync(entity);

            return Json("gay");
        }

        //public async Task<ActionResult> CheckConStatus()
        //{

        //}

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
