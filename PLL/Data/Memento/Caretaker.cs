using Newtonsoft.Json;
using PLL.Data.Entity;
using PLL.Data.Memento.Interfaces;

namespace PLL.Data.Memento
{
    public class Caretaker
    {
        [JsonProperty]
        private List<MementoTraining> _mementoes = new List<MementoTraining>();
        [JsonProperty]
        private Training _training;

        public Caretaker(Training training)
        {
            _training = training;
        }

        public void Backup()
        {
            _mementoes.Add(_training.Save() as MementoTraining);
        }

        public Training GetLast()
        {
            return _training;
        }

        public Training Undo()
        {
            if (_mementoes.Count <= 0)
            {
                return null;
            }

            var memento = _mementoes.Last();
            _mementoes.Remove(memento);

            _training.Restore(memento);

            return _training;
        }
    }
}
