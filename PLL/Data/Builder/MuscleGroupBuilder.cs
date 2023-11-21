using PLL.Data.Builder.Interface;
using PLL.Data.Entity;

namespace PLL.Data.Builder
{
    public class MuscleGroupBuilder : IEntityBuilder
    {
        private MuscleGroup _muscleGroup;

        public MuscleGroupBuilder()
        {
            _muscleGroup = new MuscleGroup();
        }

        public MuscleGroupBuilder AddId(string id)
        {
          _muscleGroup.Id = id;

            return this;
        }

        public MuscleGroupBuilder AddGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("The value must not be null or empty");

            _muscleGroup.GroupName = groupName;

            return this;
        }

        public MuscleGroupBuilder AddExercise(Exercise exercise)
        {
            _muscleGroup.Exercises.Add(exercise);

            return this;
        }

        public MuscleGroupBuilder AddTraining(Training training)
        {
            _muscleGroup.Trainings.Add(training);

            return this;
        }

        public MuscleGroup Build()
        {
            return _muscleGroup;
        }

        public void Reset()
        {
            _muscleGroup = new MuscleGroup();
        }
    }
}
