namespace PLL.Data.Entity
{
    public class MuscleGroup : IEntity
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public string TrainingId { get; set; }
        public Training Training { get; set; } 
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
