using Newtonsoft.Json;
using PLL.Data.Entity;
using Newtonsoft.Json;
using PLL.Data.Memento.Interfaces;

namespace PLL.Data.Memento
{
    public class MementoTraining : IMemento<Training>
    {
        [JsonProperty]
        private readonly Training _entity;
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }

        public MementoTraining()
        {

        }

        public MementoTraining(string id,DateTime date, List<MuscleGroup> muscleGroups)
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;

            var muscleGroupsJson = JsonConvert.SerializeObject(muscleGroups);

            var muscleGroupsList = JsonConvert.DeserializeObject<List<MuscleGroup>>(muscleGroupsJson);

            _entity = new Training
            {
                Id = id,
                Date = date,
                MuscleGroups = muscleGroupsList
            };
        }

        public Training GetEntity()
        {
            return _entity;
        }
    }
}
