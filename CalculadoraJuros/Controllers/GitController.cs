using CalculadoraJuros.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CalculadoraJuros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GitController(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        [HttpGet("showmethecode")]
        public IActionResult ShowMeTheCode()
        {
            string githubUrl = _configuration["GITHUB_CODE_URL"];
            return Ok(githubUrl);
        }
    }
}
