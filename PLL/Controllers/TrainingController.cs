using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Memento;
using PLL.Services.Interfaces;

namespace PLL.Controllers
{
    [Route("[controller]")]
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet("overview-training")]
        public async Task<IActionResult> OverviewTrainingAsync()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
                Console.WriteLine("Auth");

            var training = await _trainingService.OverviewTrainingAsync();

            return View(training);
        }

        [HttpPost("add-muscle-group")]
        public async Task<IActionResult> AddMuscleGroupAsync([FromForm] string muscleGroupName)
        {
            await _trainingService.AddMuscleGroupAsync(muscleGroupName);

            return RedirectToAction("OverviewTraining","Training");
        }

        [HttpPost("add-exercise")]
        public async Task<IActionResult> AddExerciseAsync([FromForm] string exerciseName, [FromForm] string muscleGroupId)
        {
            await _trainingService.AddExerciseAsync(exerciseName, muscleGroupId);

            return RedirectToAction("OverviewTraining", "Training");
        }

        [HttpPost("add-set")]
        public async Task<IActionResult> AddSetAsync([FromForm] string exerciseId, [FromForm] int numberRepetition,
            [FromForm] int weight, [FromForm] string unitName, [FromForm] string muscleGroupId)
        {
            await _trainingService.AddSetAsync(exerciseId, numberRepetition, weight, unitName, muscleGroupId);

            return RedirectToAction("OverviewTraining", "Training");
        }

        [HttpPost("undo")]
        public async Task<IActionResult> UndoAsync()
        {
            await _trainingService.UndoAsync();

            return RedirectToAction("OverviewTraining", "Training");
        }
    }
}
