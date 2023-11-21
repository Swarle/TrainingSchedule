using PLL.Data.Builder.Interface;
using PLL.Data.Entity;

namespace PLL.Data.Builder
{
    public class SetBuilder : IEntityBuilder
    {
        private Set _set;

        public SetBuilder()
        {
            _set = new Set();
        }

        public SetBuilder AddId(string id)
        {
            if (!Guid.TryParse(id, out _))
                throw new ArgumentException("The value must be type of Guid");

            _set.Id = id;

            return this;
        }

        public SetBuilder AddNumberRepetition(int numberRepetition)
        {
            if (!int.IsPositive(numberRepetition))
                throw new ArgumentException("The value must not be negative");

            _set.NumberRepetitons = numberRepetition;
            
            return this;
        }

        public SetBuilder AddWeight(int weight)
        {
            if (!int.IsPositive(weight))
                throw new ArgumentException("The value mast not be negative");

            _set.Weight = weight;

            return this;
        }

        public SetBuilder AddUnitId(string id)
        {
            _set.UnitId = id;

            return this;
        }

        public SetBuilder AddUnit(Unit unit)
        {
            _set.UnitId = unit.Id;
            _set.Unit = unit;

            return this;
        }

        public SetBuilder AddExerciseId(string id)
        {
            _set.ExerciseId = id;

            return this;
        }

        public SetBuilder AddExercise(Exercise exercise)
        {
            _set.ExerciseId = exercise.Id;
            _set.Exercise = exercise;

            return this;
        }

        public Set Build()
        {
            return _set;
        }

        public void Reset()
        {
            _set = new Set();
        }
    }
}
