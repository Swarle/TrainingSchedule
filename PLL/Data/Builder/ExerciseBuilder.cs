using PLL.Data.Builder.Interface;
using PLL.Data.Entity;

namespace PLL.Data.Builder
{
    public class ExerciseBuilder : IEntityBuilder
    {
        private Exercise _exercise;

        public ExerciseBuilder()
        {
            _exercise = new Exercise();
        }

        public ExerciseBuilder AddId(string id)
        {
            _exercise.Id = id;

            return this;
        }

        public ExerciseBuilder AddExerciseName(string exerciseName)
        {
            if (string.IsNullOrEmpty(exerciseName))
                throw new ArgumentException("The value must not be null or empty");

            _exercise.ExerciseName = exerciseName;
            
            return this;
        }

        public ExerciseBuilder AddMuscleGroupId(string id)
        {
            _exercise.MuscleGroupId = id;

            return this;
        }

        public ExerciseBuilder AddMuscleGroup(MuscleGroup muscleGroup)
        {
            _exercise.MuscleGroupId = muscleGroup.Id;

            _exercise.MuscleGroup = muscleGroup;

            return this;
        }

        public ExerciseBuilder AddSet(Set set)
        {
            _exercise.Sets.Add(set);

            return this;
        }

        public Exercise Build()
        {
            return _exercise;
        }

        public void Reset()
        {
            _exercise = new Exercise();
        }
    }
}
