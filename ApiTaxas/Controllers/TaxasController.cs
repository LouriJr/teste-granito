using ApiTaxas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaxas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxasController : ControllerBase
    {
        private readonly ITaxasService _taxasService;
        public TaxasController(ITaxasService taxasService)
        {
            _taxasService = taxasService;
        }
        [HttpGet]
        public IActionResult GetTaxaJuros()
        {
            var taxaJuros = _taxasService.GetTaxaJuros();
            return Ok(taxaJuros);
        }
    }
}
