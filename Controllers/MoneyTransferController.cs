using AbcMoneyTransfer.Models;
using AbcMoneyTransfer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbcMoneyTransfer.Controllers
{
    public class MoneyTransferController : Controller
    {
        private readonly ExchangeRateService _exchangeRateService;
        private readonly MoneyTransferService _moneyTransferService;
        private readonly ILogger<MoneyTransferController> _logger;
        public MoneyTransferController(ExchangeRateService exchangeRateService, MoneyTransferService moneyTransferService, ILogger<MoneyTransferController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _moneyTransferService = moneyTransferService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateTransfer()
        {
            
            var exchangeRate = await _exchangeRateService.GetExchangeRateMYRtoNPR();

            if (exchangeRate == null)
            {
                return View("Error");
            }

            // Pre-fill the form with the exchange rate
            var model = new MoneyTransferModel
            {
                ExchangeRate = exchangeRate.Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransfer(MoneyTransferModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }


            try
            {
                model.PayoutAmountNPR = model.TransferAmountMYR * model.ExchangeRate;
                await _moneyTransferService.ProcessTransferAsync(model);
                return RedirectToAction("TransferConfirmation", model);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex.Message);

               
                ModelState.AddModelError("", " Please try again later.");

                return RedirectToAction("TransferConfirmation", model);
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransferAjax(MoneyTransferModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Please correct the errors in the form." });
            }

            try
            {
                model.PayoutAmountNPR = model.TransferAmountMYR * model.ExchangeRate;
                await _moneyTransferService.ProcessTransferAsync(model);
                return Json(new { success = true, message = "Successfully submitted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { success = false, message = "Please try again later." });
            }
        }

        [HttpGet]
        public IActionResult TransferConfirmation(MoneyTransferModel model)
        {
            return View(model);
        }
    }
}
