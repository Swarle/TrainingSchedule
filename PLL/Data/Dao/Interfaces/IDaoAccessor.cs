using Microsoft.AspNetCore.DataProtection.Repositories;
using PLL.Data.Entity;

namespace PLL.Data.Dao.Interfaces
{
    public interface IDaoAccessor
    {
        IDao<Training> TrainingDao { get; }
        IDao<Exercise> ExerciseDao { get; }
        IDao<MuscleGroup> MuscleGroupDao { get; }
        IDao<Set> SetDao { get; }
        IDao<Unit> UnitDao { get; }
        IDao<User> UserDao { get;}
        IDao<Role> RoleDao { get; }
    }
}
