using Models;

namespace Domain.Interfaces
{
    public interface IPocRepository
    {
        IEnumerable<Poc> GetPocList();
    }
}
