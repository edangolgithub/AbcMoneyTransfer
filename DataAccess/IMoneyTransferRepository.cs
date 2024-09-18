using AbcMoneyTransfer.Models;

namespace AbcMoneyTransfer.DataAccess
{
    public interface IMoneyTransferRepository
    {
        Task AddAsync(MoneyTransferModel transfer);
        Task<MoneyTransferModel?> GetByIdAsync(int id);
        Task<IEnumerable<MoneyTransferModel>> GetAllAsync();
        Task SaveChangesAsync();
    }

}
