namespace PLL.Data.Memento.Interfaces
{
    public interface IMemento<TEntity>
    {
        Guid Id { get; set; }
        DateTime DateTime { get; set; }
        TEntity GetEntity();
    }
}
