using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;

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
            Training training;

            if (HttpContext.Session.Keys.Contains("training"))
            {
                training = JsonConvert.DeserializeObject<Training>(HttpContext.Session.GetString("training"));
            }
            else
            {
                training = new Training
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now
                };
            }
            
            var jsonTraining = JsonConvert.SerializeObject(training);

            HttpContext.Session.SetString("training",jsonTraining);

            return View(training);
        }

        [HttpPost("add-muscle-group")]
        public IActionResult AddMuscleGroup([FromForm] string muscleGroupName)
        {
            var trainingString = HttpContext.Session.GetString("training");

            var training = JsonConvert.DeserializeObject<Training>(trainingString);

            training.MuscleGroups.Add(new MuscleGroup
            {
                Id = Guid.NewGuid().ToString(),
                GroupName = muscleGroupName
            });

            HttpContext.Session.SetString("training",JsonConvert.SerializeObject(training));

            return RedirectToAction("OverviewTraining","Training");
        }

        [HttpPost("add-exercise")]
        public IActionResult AddExercise([FromForm] string exerciseName, [FromForm] string muscleGroupId)
        {
            var trainingString = HttpContext.Session.GetString("training");

            var training = JsonConvert.DeserializeObject<Training>(trainingString);

            training.MuscleGroups.Where(e => e.Id == muscleGroupId).FirstOrDefault().Exercises.Add(new Exercise
            {
                Id = Guid.NewGuid().ToString(),
                ExerciseName = exerciseName,
                MuscleGroupId = muscleGroupId
            });

            HttpContext.Session.SetString("training",JsonConvert.SerializeObject(training));

            return RedirectToAction("OverviewTraining", "Training");
        }

        [HttpPost("add-set")]
        public IActionResult AddSet([FromForm] string exerciseId, [FromForm] int numberRepetition,
            [FromForm] int weight, [FromForm] string unitName, [FromForm] string muscleGroupId)
        {
            var trainingString = HttpContext.Session.GetString("training");

            var training = JsonConvert.DeserializeObject<Training>(trainingString);

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

            HttpContext.Session.SetString("training", JsonConvert.SerializeObject(training));

            return RedirectToAction("OverviewTraining", "Training");
        }
    }
}
