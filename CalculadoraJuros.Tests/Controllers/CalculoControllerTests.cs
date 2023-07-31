using CalculadoraJuros.Controllers;
using CalculadoraJuros.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraJuros.Tests.Controllers
{
    public class CalculoControllerTests
    {
        private readonly Mock<ICalculoService> _calculoServiceMock;
        private readonly CalculoController _calculoController;

        public CalculoControllerTests()
        {
            _calculoServiceMock = new Mock<ICalculoService>();
            _calculoController = new CalculoController(_calculoServiceMock.Object);
        }

        [Fact]
        public async Task CalcularJuros_DeveRetornarOkComValorFinal()
        {
            // Arrange
            decimal valorInicial = 100;
            int meses = 5;
            decimal valorFinalEsperado = 105.10m;
            _calculoServiceMock.Setup(s => s.CalcularJuros(valorInicial, meses)).ReturnsAsync(valorFinalEsperado);

            // Act
            var response = await _calculoController.CalcularJuros(valorInicial, meses);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var valorFinalObtido = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(valorFinalEsperado, valorFinalObtido);
        }

        [Fact]
        public async Task CalcularJuros_DeveRetornarBadRequestComValorInicialInvalido()
        {
            // Arrange
            decimal valorInicial = 0;
            int meses = 5;

            // Act
            var response = await _calculoController.CalcularJuros(valorInicial, meses);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("O valor inicial fornecido na requisição é inválido. Por favor, forneça um valor válido!", errorMessage);
        }

        [Fact]
        public async Task CalcularJuros_DeveRetornarBadRequestComMesesInvalidos()
        {
            // Arrange
            decimal valorInicial = 100;
            int meses = 0;

            // Act
            var response = await _calculoController.CalcularJuros(valorInicial, meses);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("O mês fornecido na requisição é inválido. Por favor, forneça um valor válido para o mês!", errorMessage);
        }
    }
}
