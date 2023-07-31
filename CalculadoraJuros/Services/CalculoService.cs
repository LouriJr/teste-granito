using CalculadoraJuros.Extensions;
using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace CalculadoraJuros.Services
{
    public interface ICalculoService
    {
        Task<decimal> CalcularJuros(decimal valorinicial, int meses);
    }
    public class CalculoService : ICalculoService
    {
        private readonly string _apiTaxaURL;
        private readonly IHttpClientFactory _httpClientFactory;

        public CalculoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiTaxaURL = configuration["TAXA_API_URL"];
        }

        public async Task<decimal> CalcularJuros(decimal valorinicial, int meses)
        {
            decimal taxaJuros = await GetTaxaJurosFromApi();
            decimal valorFinal = valorinicial * (decimal)Math.Pow(1 + (double)taxaJuros, meses);
            valorFinal = valorFinal.TruncateDecimal(decimalPlaces: 2);

            return valorFinal;
        }

        private async Task<decimal> GetTaxaJurosFromApi()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var requestUrl = $"{_apiTaxaURL}api/taxas";
            var response = await httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Ocorreu um erro ao obter a taxa de juros da API. Por favor, tente novamente mais tarde.");
            }
            var taxaJurosStr = await response.Content.ReadAsStringAsync();

            if (decimal.TryParse(taxaJurosStr, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal taxaJuros))
            {
                return taxaJuros;
            }

            throw new Exception("Ocorreu um erro ao obter a taxa de juros da API. Por favor, tente novamente mais tarde.");
        }
    }
}
