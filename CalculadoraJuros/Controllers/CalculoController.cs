using CalculadoraJuros.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraJuros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {
        private readonly ICalculoService _calculoService;
        public CalculoController(ICalculoService calculoService)
        {
            _calculoService = calculoService;
        }

        [HttpGet("calculajuros")]
        public async Task<IActionResult> CalcularJuros(decimal valorInicial, int meses)
        {
            if (valorInicial <= 0) { return BadRequest("O valor inicial fornecido na requisição é inválido. Por favor, forneça um valor válido!"); }
            if (meses <= 0) { return BadRequest("O mês fornecido na requisição é inválido. Por favor, forneça um valor válido para o mês!"); }

            var valorFinal = await _calculoService.CalcularJuros(valorInicial, meses);
            return Ok(valorFinal);
        }
    }
}
