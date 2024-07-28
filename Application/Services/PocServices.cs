using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public PocViewModel GetPocList()
        {
            return new PocViewModel()
            {
                PocList = _pocRepository.GetPocList()
            };
        }
    }
}
