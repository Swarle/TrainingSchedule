using PLL.Data.Dao.SqlDao;

namespace PLL.Data.Observer.Interfaces
{
    public interface ISubject
    {
        DaoState _state { get; set; }
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
