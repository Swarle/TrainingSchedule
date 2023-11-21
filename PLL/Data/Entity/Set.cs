namespace PLL.Data.Entity
{
    public class Set : IEntity
    {
        public string Id { get; set; }
        public int NumberRepetitons { get; set; }
        public int Weight { get; set; }
        public string UnitId { get; set; }
        public string ExerciseId { get; set; }

        public Unit Unit { get; set; }
        public Exercise Exercise { get; set; }
    }
}
