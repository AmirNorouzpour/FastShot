using Application.Interfaces;
using Domain.Interfaces;
using ViewModels;

namespace Application.Services
{
    public class PocServices : IPocService
    {
        private IPocRepository _pocRepository;
        public PocServices(IPocRepository pocRepository)
        {
            _pocRepository = pocRepository;
        }
        public IEnumerable<PocViewModel> GetPocList()
        {
            return _pocRepository.GetPocList().Select(x => new PocViewModel
            {
                Id = x.Id,
                Username = x.Username
            });

        }
    }
}
