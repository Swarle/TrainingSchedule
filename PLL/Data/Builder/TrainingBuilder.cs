using PLL.Data.Builder.Interface;
using PLL.Data.Entity;

namespace PLL.Data.Builder
{
    public class TrainingBuilder : IEntityBuilder
    {
        private Training _training;

        public TrainingBuilder()
        {
            _training = new Training();
        }

        public TrainingBuilder AddId(string id)
        {
            _training.Id = id;

            return this;
        }

        public TrainingBuilder AddDate(DateTime date)
        {
            _training.Date = date;

            return this;
        }

        public TrainingBuilder AddMuscleGroup(MuscleGroup muscleGroup)
        {
            _training.MuscleGroups.Add(muscleGroup);

            return this;
        }

        public TrainingBuilder AddMuscleGroups(List<MuscleGroup> muscleGroups)
        {
            _training.MuscleGroups.AddRange(muscleGroups);

            return this;
        }

        public Training Build()
        {
            return _training;
        }

        public void Reset()
        {
            _training = new Training();
        }
    }
}
