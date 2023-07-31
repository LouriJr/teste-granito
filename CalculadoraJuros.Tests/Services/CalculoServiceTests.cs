using CalculadoraJuros.Controllers;
using CalculadoraJuros.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraJuros.Tests.Services
{
    public class CalculoServiceTests
    {
        private readonly CalculoService _calculoService;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;

        public CalculoServiceTests()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.SetupGet(x => x["TAXA_API_URL"]).Returns("https://api-taxas-fake.azurewebsites.net/");

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _calculoService = new CalculoService(_httpClientFactoryMock.Object, configuration.Object);
        }

        [Theory]
        [InlineData(100, 5, 105.10)]
        [InlineData(200, 3, 206.06)]
        public async Task CalcularJuros_DeveCalcularCorretamente(decimal valorInicial, int meses, decimal valorEsperado)
        {
            // Arrange
            var httpClient = new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("0.01")
            }));

            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var resultado = await _calculoService.CalcularJuros(valorInicial, meses);

            // Assert
            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public async Task CalcularJuros_DeveLancarExcecaoQuandoFalharAoObterTaxaDeJuros()
        {
            // Arrange
            var httpClient = new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Erro na API de Taxas")
            }));

            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act e Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _calculoService.CalcularJuros(100, 5));
        }
    }

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_response);
        }
    }
}
