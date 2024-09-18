using AbcMoneyTransfer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbcMoneyTransfer.Controllers
{
    public class ExchangeRateController : Controller
    {
        private readonly ExchangeRateService _exchangeRateService;

        public ExchangeRateController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        //https://localhost:7113/ExchangeRate/GetExchangeRate


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var exchangeRates = await _exchangeRateService.GetExchangeRates();
            if (exchangeRates == null)
            {
                return View("Error"); 
            }

            return View(exchangeRates);
        }
        [HttpGet]
        public async Task<IActionResult> GetExchangeRate()
        {
            var myrToNprRate = await _exchangeRateService.GetExchangeRateMYRtoNPR();

            if (myrToNprRate == null)
            {
                return View("Error"); // Show error view if fetching failed
            }

            ViewBag.MYRtoNPRRate = myrToNprRate;
            return View();
        }
    }
}