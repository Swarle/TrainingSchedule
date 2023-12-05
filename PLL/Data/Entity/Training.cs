using PLL.Data.Memento;
using PLL.Data.Memento.Interfaces;

namespace PLL.Data.Entity
{
    public class Training : IEntity
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<MuscleGroup>? MuscleGroups { get; set; } = new List<MuscleGroup>();

        public IMemento<Training> Save()
        {
            return new MementoTraining(Id,Date,MuscleGroups);
        }

        public void Restore(IMemento<Training> memento)
        {
            var entity = memento.GetEntity();

            Id = entity.Id;
            Date = entity.Date;
            MuscleGroups = entity.MuscleGroups;
        }
    }
}
