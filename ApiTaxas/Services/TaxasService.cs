using Microsoft.AspNetCore.Mvc;

namespace ApiTaxas.Services
{
    public interface ITaxasService
    {
        decimal GetTaxaJuros();
    }
    public class TaxasService : ITaxasService
    {
        public decimal GetTaxaJuros()
        {
            return 0.01m;
        }
    }
}
