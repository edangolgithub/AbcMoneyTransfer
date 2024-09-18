namespace AbcMoneyTransfer.Models
{
    public class ExchangeRateViewModel
    {
        public List<ExchangeRate> Rates { get; set; }
        public string? Today { get; set; }
    }

    public class ExchangeRate
    {
        public string Iso3 { get; set; }
        public int Unit { get; set; }
        public string Name { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public string FlagUrl { get; set; } // URL to the flag image
    }
}
