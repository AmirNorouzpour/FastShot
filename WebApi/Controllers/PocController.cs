
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PocController : ControllerBase
    {
        private IPocService _pocService;
        public PocController(IPocService pocService)
        {
            _pocService = pocService;
        }


        [HttpGet]
        public IEnumerable<PocViewModel?> Get()
        {
            var pocVm = _pocService.GetPocList();
            return pocVm;
        }
    }
}
