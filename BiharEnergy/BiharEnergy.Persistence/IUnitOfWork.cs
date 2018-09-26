using System.Threading.Tasks;

namespace BiharEnergy.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}