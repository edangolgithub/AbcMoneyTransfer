using AbcMoneyTransfer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbcMoneyTransfer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<MoneyTransferModel> MoneyTransfers { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MoneyTransferModel>(entity =>
            {
                // Configure precision and scale for decimal properties
                entity.Property(e => e.TransferAmountMYR)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ExchangeRate)
                    .HasColumnType("decimal(18,6)"); // Adjust precision and scale as needed

                entity.Property(e => e.PayoutAmountNPR)
                    .HasColumnType("decimal(18,2)");
            });

        }
       
    }
}
