using AbcMoneyTransfer.Data;
using AbcMoneyTransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace AbcMoneyTransfer.Services
{
    public interface ITransactionReportService
    {
        Task<IEnumerable<MoneyTransferModel>> GetTransactionReportsAsync(DateTime startDate, DateTime endDate);
    }

    public class TransactionReportService : ITransactionReportService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionReportService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MoneyTransferModel>> GetTransactionReportsAsync(DateTime startDate, DateTime endDate)
        {
            var endDateTime = endDate.Date.AddDays(1).AddTicks(-1); // at end

            return await _dbContext.MoneyTransfers
                .Where(t => t.TransferDate >= startDate.Date && t.TransferDate <= endDateTime)
                .Select(t => new MoneyTransferModel
                {
                    Id = t.Id,
                    SenderFirstName = t.SenderFirstName,
                    ReceiverFirstName = t.ReceiverFirstName,
                    PayoutAmountNPR = t.PayoutAmountNPR,
                    BankName= t.BankName,
                    TransferAmountMYR = t.TransferAmountMYR,
                    AccountNumber = t.AccountNumber,
                    TransferDate = t.TransferDate
                })
                .ToListAsync();

        }
    }
}
