namespace PLL.Data.Observer.Interfaces
{
    public interface IObserver
    {
        Task Update(ISubject subject);
    }
}
