namespace PLL.Data.Entity
{
    public class MuscleGroup : IEntity
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public List<Training> Trainings { get; set; } = new List<Training>();
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
