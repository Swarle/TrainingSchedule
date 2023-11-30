using Newtonsoft.Json;

namespace PLL.Data.Entity
{
    public class Exercise : IEntity
    {
        public string Id { get; set; }
        public string ExerciseName { get; set; }
        public string MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();

    }
}
