using AbcMoneyTransfer.Data;
using AbcMoneyTransfer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AbcMoneyTransfer.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ITransactionReportService _reportService;

        public ReportsController(ITransactionReportService reportService)
        {
           
            _reportService = reportService;
        }

        public async Task<IActionResult> ExportToCsvAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _reportService.GetTransactionReportsAsync(startDate, endDate);
                

            var csv = new StringBuilder();
            csv.AppendLine("Sender,Receiver,Bank Name,Account Number,Transfer Amount (MYR),Exchange Rate,Payout Amount (NPR),Date");

            foreach (var transaction in transactions)
            {
                csv.AppendLine($"{transaction.SenderFirstName} {transaction.SenderLastName}," +
                                $"{transaction.ReceiverFirstName} {transaction.ReceiverLastName}," +
                                $"{transaction.BankName}," +
                                $"{transaction.AccountNumber}," +
                                $"{transaction.TransferAmountMYR}," +
                                $"{transaction.ExchangeRate}," +
                                $"{transaction.PayoutAmountNPR}," +
                                $"{transaction.TransferDate.ToShortDateString()}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var result = new FileContentResult(bytes, "text/csv")
            {
                FileDownloadName = "TransactionReport.csv"
            };

            return result;
        }

        //public async Task<IActionResult> ExportToExcelAsync(DateTime startDate, DateTime endDate)
        //{
        //    var transactions = await _reportService.GetTransactionReportsAsync(startDate, endDate);

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Transactions");
        //        worksheet.Cells["A1"].LoadFromCollection(transactions, true);

        //        var stream = new MemoryStream();
        //        package.SaveAs(stream);
        //        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransactionReport.xlsx");
        //    }
        //}
        //public async Task<IActionResult> GeneratePdfReportAsync(DateTime startDate, DateTime endDate)
        //{
        //    var transactions = await _reportService.GetTransactionReportsAsync(startDate, endDate);
        //    return new ViewAsPdf("Report", transactions)
        //    {
        //        FileName = "TransactionReport.pdf"
        //    };
        //}

        [HttpGet]
        public async Task<IActionResult> GenerateReportAsync(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
               
                var reports = await _reportService.GetTransactionReportsAsync(startDate.Value, endDate.Value);
               
                ViewBag.StartDate = startDate.Value;
                ViewBag.EndDate = endDate.Value;
                return View(reports);
            }

            
            return View();
        }
    }

}
