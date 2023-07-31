using ApiTaxas.Controllers;
using ApiTaxas.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Taxas.Test.Controllers
{
    public class TaxasControllerTests
    {
        private readonly TaxasController _taxasController;
        private readonly Mock<ITaxasService> _taxasServiceMock;

        public TaxasControllerTests()
        {
            _taxasServiceMock = new Mock<ITaxasService>();
            _taxasController = new TaxasController(_taxasServiceMock.Object);
        }

        [Fact]
        public void GetTaxaJuros_DeveRetornarOkComTaxaJuros()
        {
            // Arrange
            decimal taxaJurosEsperada = 0.01m;
            _taxasServiceMock.Setup(x => x.GetTaxaJuros()).Returns(taxaJurosEsperada);

            // Act
            var response = _taxasController.GetTaxaJuros();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var taxaJurosObtida = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(taxaJurosEsperada, taxaJurosObtida);
        }
    }
}
