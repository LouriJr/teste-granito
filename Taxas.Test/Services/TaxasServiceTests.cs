using ApiTaxas.Services;

namespace Taxas.Test.Services
{
    public class TaxasServiceTests
    {
        [Fact]
        public void GetTaxaJuros_DeveRetornarTaxaJurosEsperada()
        {
            // Arrange
            var taxasService = new TaxasService();

            // Act
            var taxaJuros = taxasService.GetTaxaJuros();

            // Assert
            decimal taxaJurosEsperada = 0.01m;
            Assert.Equal(taxaJurosEsperada, taxaJuros);
        }
    }
}
