namespace AbcMoneyTransfer.Models
{
    public class ExchangeRateApiResponse
    {
        public Data? data { get; set; }

        public class Data
        {
            public List<Payload>? payload { get; set; }
        }

        public class Payload
        {
            public List<Rate>? rates { get; set; }
        }

        public class Rate
        {
            public Currency? currency { get; set; }
            public string? buy { get; set; }
            public string? sell { get; set; }
        }

        public class Currency
        {
            public string? iso3 { get; set; }
            public string? name { get; set; }
            public int unit { get; set; }
        }
    }

}
