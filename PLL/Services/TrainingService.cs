using Newtonsoft.Json;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Memento;
using PLL.Services.Interfaces;

namespace PLL.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly HttpContext _context;
        private readonly IDaoAccessor _daoAccessor;

        public TrainingService(IHttpContextAccessor accessor,IDaoAccessor daoAccessor)
        {
            _context = accessor.HttpContext;
            _daoAccessor = daoAccessor;
        }

        public Task<Training> OverviewTrainingAsync()
        {
            var task = Task.Run(() =>
            {
                Caretaker caretaker;
                Training training;

                if (_context.Session.Keys.Contains("caretaker"))
                {
                    caretaker = JsonConvert.DeserializeObject<Caretaker>(_context.Session.GetString("caretaker"));

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

                _context.Session.SetString("caretaker", jsonCaretaker);

                return training;
            });

            return task;
        }

        public Task AddMuscleGroupAsync(string muscleGroupName)
        {
            return Task.Run(() =>
            {
                var caretakerString = _context.Session.GetString("caretaker");

                var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

                var training = caretaker.GetLast();

                caretaker.Backup();

                training.MuscleGroups.Add(new MuscleGroup
                {
                    Id = Guid.NewGuid().ToString(),
                    GroupName = muscleGroupName
                });

                _context.Session.SetString("caretaker", JsonConvert.SerializeObject(caretaker));
            });
        }

        public Task AddExerciseAsync(string exerciseName, string muscleGroupId)
        {
            return Task.Run(() =>
            {
                var caretakerString = _context.Session.GetString("caretaker");

                var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

                var training = caretaker.GetLast();

                caretaker.Backup();

                training.MuscleGroups.Where(e => e.Id == muscleGroupId).FirstOrDefault().Exercises.Add(new Exercise
                {
                    Id = Guid.NewGuid().ToString(),
                    ExerciseName = exerciseName,
                    MuscleGroupId = muscleGroupId
                });

                _context.Session.SetString("caretaker", JsonConvert.SerializeObject(caretaker));
            });
        }

        public Task AddSetAsync(string exerciseId, int numberRepetition,
            int weight, string unitName, string muscleGroupId)
        {
            return Task.Run(() =>
            {
                var caretakerString = _context.Session.GetString("caretaker");

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

                _context.Session.SetString("caretaker", JsonConvert.SerializeObject(caretaker));
            });
        }

        public Task UndoAsync()
        {
            return Task.Run(() =>
            {
                var caretakerString = _context.Session.GetString("caretaker");

                var caretaker = JsonConvert.DeserializeObject<Caretaker>(caretakerString);

                var training = caretaker.Undo();

                _context.Session.SetString("caretaker", JsonConvert.SerializeObject(caretaker));
            });
        }
    }
}
