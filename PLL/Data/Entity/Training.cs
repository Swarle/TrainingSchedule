namespace PLL.Data.Entity
{
    public class Training : IEntity
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<MuscleGroup> MuscleGroups { get; set; } = new List<MuscleGroup>();
    }
}
