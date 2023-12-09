using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Observer.Interfaces;

namespace PLL.Data.Dao.MongoDbDao
{
    public class MongoDbAccessor : IDaoAccessor
    {
        private IDao<Training>? _trainingDao;
        private IDao<Exercise>? _exerciseDao;
        private IDao<MuscleGroup>? _muscleGroupDao;
        private IDao<Set>? _setDao;
        private IDao<Unit>? _unitDao;
        private IDao<Role>? _roleDao;
        private IDao<User>? _userDao;

        private readonly ILoggerFactory _loggerFactory;

        public MongoDbAccessor(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public IDao<Training> TrainingDao
        {
            get
            {
                if (_trainingDao == null)
                    return _trainingDao = new TrainingDao(_loggerFactory.CreateLogger<TrainingDao>());

                return _trainingDao;
            }

        }

        public IDao<Exercise> ExerciseDao
        {
            get
            {
                if (_exerciseDao == null)
                    return _exerciseDao = new ExerciseDao(_loggerFactory.CreateLogger<ExerciseDao>());

                return _exerciseDao;
            }
        }

        public IDao<MuscleGroup> MuscleGroupDao
        {
            get
            {
                if (_muscleGroupDao == null)
                    return _muscleGroupDao = new MuscleGroupDao(_loggerFactory.CreateLogger<MuscleGroupDao>());

                return _muscleGroupDao;
            }
        }

        public IDao<Set> SetDao
        {
            get
            {
                if (_setDao == null)
                    return _setDao = new SetDao(_loggerFactory.CreateLogger<SetDao>());

                return _setDao;
            }
        }

        public IDao<Unit> UnitDao
        {
            get
            {
                if (_unitDao == null)
                    return _unitDao = new UnitDao(_loggerFactory.CreateLogger<UnitDao>());

                return _unitDao;
            }
        }

        public IDao<User> UserDao
        {
            get
            {
                if (_userDao == null)
                    return _userDao = new UserDao(_loggerFactory.CreateLogger<UserDao>());

                return _userDao;
            }
        }

        public IDao<Role> RoleDao
        {
            get
            {
                if (_roleDao == null)
                    return _roleDao = new RoleDao(_loggerFactory.CreateLogger<RoleDao>());

                return _roleDao;
            }
        }
    }
}
