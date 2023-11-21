using PLL.Data.Builder.Interface;
using PLL.Data.Entity;

namespace PLL.Data.Builder
{
    public class UnitBuilder : IEntityBuilder
    {
        private Unit _unit;

        public UnitBuilder()
        {
            _unit = new Unit();
        }

        public UnitBuilder AddId(string id)
        {
            _unit.Id = id;

            return this;
        }

        public UnitBuilder AddUnitName(string unitName)
        {
            if (string.IsNullOrEmpty(unitName)) throw new ArgumentException("The value must not be null or empty");

            _unit.UnitName = unitName;

            return this;
        }

        public Unit Build()
        {
            return _unit;
        }

        public void Reset()
        {
            _unit = new Unit();
        }
    }
}
