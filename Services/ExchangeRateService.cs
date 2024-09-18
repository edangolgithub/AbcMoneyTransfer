using AbcMoneyTransfer.Models;
using Newtonsoft.Json;

namespace AbcMoneyTransfer.Services
{
 

    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://www.nrb.org.np/api/forex/v1/rates";
        private readonly string _today = DateTime.Today.ToString("yyyy-MM-dd");
        private int page = 1;
        private int pageSize = 5;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiUrl = $"{_apiUrl}?page={page}&per_page={pageSize}&from={_today}&to={_today}";
        }

        public async Task<decimal?> GetExchangeRateMYRtoNPR()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}");
           
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(content);

          
            var myrRate = apiResponse.data.payload
                .FirstOrDefault()
                ?.rates.FirstOrDefault(r => r.currency.iso3 == "MYR")?.buy;

            return myrRate != null ? decimal.Parse(myrRate) : (decimal?)null;
        }

        internal async Task<ExchangeRateViewModel?> GetExchangeRates()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}");

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                return null; 
            }

         
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(content);

            // Check deserialization 
            if (apiResponse == null || apiResponse.data == null || apiResponse.data.payload == null)
            {
                return null;
            }

           
            var exchangeRates = apiResponse.data.payload.SelectMany(p => p.rates.Select(rate => new ExchangeRate
            {
                Iso3 = rate.currency.iso3,
                Name = rate.currency.name,
                Unit = rate.currency.unit,
                Buy = decimal.Parse(rate.buy),
                Sell = decimal.Parse(rate.sell),
                
            })).ToList();

            return new ExchangeRateViewModel
            {
                Rates = exchangeRates,
                Today = DateTime.Today.ToString("D")
            };
        }
            
    }

}
