namespace WebApp.Models
{
    public class Stock
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ClosingPrice { get; set; } = string.Empty;

        public string MonthlyAveragePrice { get; set; } = string.Empty;
    }
}