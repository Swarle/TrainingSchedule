using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Memento;

namespace PLL.Controllers
{
    [Route("[controller]")]
    public class TrainingController : Controller
    {
        private readonly IDaoAccessor _accessor;

        public TrainingController(IDaoAccessor accessor)
        {
            _accessor = accessor;
        }

        [HttpGet("overview-training")]
        public IActionResult OverviewTraining()
        {
            Caretaker caretaker;
            Training training;

            if (HttpContext.Session.Keys.Contains("caretaker"))
            {
                caretaker = JsonConvert.DeserializeObject<Caretaker>(HttpContext.Session.GetString("caretaker"));

                training = caretaker.GetLast();
            }
            else
            {
                training = new Training
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now
                };

                caretaker = new Caretaker(training);

            }
            

            var jsonCaretaker = JsonConvert.SerializeObject(caretaker);

            HttpContext.Session.SetString("caretaker", jsonCaretaker);

            return View(training);
        }

        [HttpPost("add-muscle-group")]
        public IActionResult AddMuscleGroup([FromForm] string muscleGroupName)
        {
            var caretakerString = HttpContext.Session.GetString("caretaker");

            var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

            var training = caretaker.GetLast();

            caretaker.Backup();

            training.MuscleGroups.Add(new MuscleGroup
            {
                Id = Guid.NewGuid().ToString(),
                GroupName = muscleGroupName
            });

            //var trainings = _accessor.TrainingDao.GetAllAsync().Result;

            HttpContext.Session.SetString("caretaker",JsonConvert.SerializeObject(caretaker));

            return RedirectToAction("OverviewTraining","Training");
        }

        [HttpPost("add-exercise")]
        public IActionResult AddExercise([FromForm] string exerciseName, [FromForm] string muscleGroupId)
        {
            var caretakerString = HttpContext.Session.GetString("caretaker");

            var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

            var training = caretaker.GetLast();

            caretaker.Backup();

            training.MuscleGroups.Where(e => e.Id == muscleGroupId).FirstOrDefault().Exercises.Add(new Exercise
            {
                Id = Guid.NewGuid().ToString(),
                ExerciseName = exerciseName,
                MuscleGroupId = muscleGroupId
            });

            HttpContext.Session.SetString("caretaker",JsonConvert.SerializeObject(caretaker));

            return RedirectToAction("OverviewTraining", "Training");
        }

        [HttpPost("add-set")]
        public IActionResult AddSet([FromForm] string exerciseId, [FromForm] int numberRepetition,
            [FromForm] int weight, [FromForm] string unitName, [FromForm] string muscleGroupId)
        {
            var caretakerString = HttpContext.Session.GetString("caretaker");

            var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

            var training = caretaker.GetLast();

            caretaker.Backup();

            var muscleGroup = training.MuscleGroups.Where(e => e.Id == muscleGroupId).FirstOrDefault();

            var exercise = muscleGroup.Exercises.Where(e => e.Id == exerciseId).FirstOrDefault();

            exercise.Sets.Add(new Set
            {
                Id = Guid.NewGuid().ToString(),
                ExerciseId = exerciseId,
                NumberRepetitons = numberRepetition,
                Weight = weight,
                Unit = new Unit
                {
                    Id = new Guid().ToString(),
                    UnitName = unitName
                }

            });

            HttpContext.Session.SetString("caretaker", JsonConvert.SerializeObject(caretaker));

            return RedirectToAction("OverviewTraining", "Training");
        }

        [HttpPost("undo")]
        public IActionResult Undo()
        {
            var caretakerString = HttpContext.Session.GetString("caretaker");

            var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

            var training = caretaker.Undo();

            HttpContext.Session.SetString("caretaker", JsonConvert.SerializeObject(caretaker));

            return RedirectToAction("OverviewTraining", "Training");
        }
    }
}
