using ViewModels;

namespace Application.Interfaces
{
    public interface IPocService
    {
        IEnumerable<PocViewModel> GetPocList();
    }
}
