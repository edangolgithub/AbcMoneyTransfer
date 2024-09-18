using AbcMoneyTransfer.DataAccess;
using AbcMoneyTransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace AbcMoneyTransfer.Services
{
    public class MoneyTransferService
    {
        private readonly IMoneyTransferRepository _repository;
        private readonly ILogger<MoneyTransferService> _logger;

        public MoneyTransferService(IMoneyTransferRepository repository, ILogger<MoneyTransferService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task ProcessTransferAsync(MoneyTransferModel model)
        {
            try
            {
              
                await _repository.AddAsync(model);
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                
                _logger.LogError(dbEx, "Database error .");
                throw;
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, " error.");
                throw;
            }
        }
    }

}
