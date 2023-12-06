using System.Net;
using System.Security.Claims;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Infostracture;
using PLL.Services;
using PLL.Services.Interfaces;

namespace PLL.Proxy
{
    public class TrainingProxyService : ITrainingService
    {
        private readonly HttpContext _context;

        private TrainingService _trainingService;

        public TrainingProxyService(IHttpContextAccessor _accessor,IDaoAccessor _daoAccessor)
        {
            _trainingService = new TrainingService(_accessor, _daoAccessor);

            _context = _accessor.HttpContext;
        }

        public Task<Training> OverviewTrainingAsync()
        {
            return _trainingService.OverviewTrainingAsync();
        }

        public Task AddMuscleGroupAsync(string muscleGroupName)
        {
            return _trainingService.AddMuscleGroupAsync(muscleGroupName);
        }

        public Task AddExerciseAsync(string exerciseName, string muscleGroupId)
        {
            return _trainingService.AddExerciseAsync(exerciseName, muscleGroupId);
        }

        public Task AddSetAsync(string exerciseId, int numberRepetition, int weight, string unitName, string muscleGroupId)
        {
            return  _trainingService.AddSetAsync(exerciseId, numberRepetition, weight, unitName, muscleGroupId);
        }

        public Task UndoAsync()
        {
            if(CheckAccess())
                return _trainingService.UndoAsync();

            throw new HttpException("Access denied", HttpStatusCode.Forbidden);
        }

        private bool CheckAccess()
        {
            return _context.User.HasClaim(ClaimTypes.Role, "Admin");
        }
    }
}
