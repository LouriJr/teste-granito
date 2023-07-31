namespace CalculadoraJuros.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal TruncateDecimal(this decimal value, int decimalPlaces)
        {
            var factor = (decimal)Math.Pow(10, decimalPlaces);
            return Math.Truncate(value * factor) / factor;
        }
    }
}
