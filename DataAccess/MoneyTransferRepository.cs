using AbcMoneyTransfer.Data;
using AbcMoneyTransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace AbcMoneyTransfer.DataAccess
{
    public class MoneyTransferRepository : IMoneyTransferRepository
    {
        private readonly ApplicationDbContext _context;

        public MoneyTransferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MoneyTransferModel transfer)
        {
            try
            {
                _context.MoneyTransfers.Add(transfer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                throw new ApplicationException("An error occurred while adding the transfer.", ex);
            }
        }

        public async Task<MoneyTransferModel?> GetByIdAsync(int id)
        {
            return await _context.MoneyTransfers.FindAsync(id);
        }

        public async Task<IEnumerable<MoneyTransferModel>> GetAllAsync()
        {
            return await _context.MoneyTransfers.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
