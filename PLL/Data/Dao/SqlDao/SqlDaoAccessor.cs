using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Observer.Interfaces;

namespace PLL.Data.Dao.SqlDao
{
    public class SqlDaoAccessor : IDaoAccessor
    {
        private IDao<Training>? _trainingDao;
        private IDao<Exercise>? _exerciseDao;
        private IDao<MuscleGroup>? _muscleGroupDao;
        private IDao<Set>? _setDao;
        private IDao<Unit>? _unitDao;
        private IDao<Role>? _roleDao;
        private IDao<User>? _userDao;

        private readonly IObserver _observer;

        private readonly ILoggerFactory _loggerFactory;

        public SqlDaoAccessor(ILoggerFactory loggerFactory, IObserver observer)
        {
            _loggerFactory = loggerFactory;
            _observer = observer;
        }

        public IDao<Training> TrainingDao
        {
            get
            {
                if (_trainingDao == null)
                {
                    _trainingDao = new TrainingDao(_loggerFactory.CreateLogger<TrainingDao>());

                    var subjectTraining = _trainingDao as ISubject;

                    subjectTraining.Attach(_observer);
                }

                return _trainingDao;
            }
        }

        public IDao<Exercise> ExerciseDao
        {
            get
            {
                if(_exerciseDao == null)
                    _exerciseDao = new ExerciseDao(_loggerFactory.CreateLogger<ExerciseDao>());

                return _exerciseDao;
            }
        }

        public IDao<MuscleGroup> MuscleGroupDao
        {
            get
            {
                if(_muscleGroupDao == null)
                    _muscleGroupDao = new MuscleGroupDao(_loggerFactory.CreateLogger<MuscleGroupDao>());

                return _muscleGroupDao;
            }
        }

        public IDao<Set> SetDao
        {
            get
            {
                if(_setDao == null)
                    _setDao = new SetDao(_loggerFactory.CreateLogger<SetDao>());

                return _setDao;
            }
        }

        public IDao<Unit> UnitDao
        {
            get
            {
                if(_unitDao == null)
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
