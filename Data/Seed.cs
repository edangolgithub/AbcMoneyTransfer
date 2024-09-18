using AbcMoneyTransfer.Controllers;
using AbcMoneyTransfer.Data.Migrations;
using AbcMoneyTransfer.Models;
using Microsoft.AspNetCore.Identity;

namespace AbcMoneyTransfer.Data
{
    public class Seed
    {
        private readonly ILogger<Seed> _logger;
        public Seed(ILogger<Seed> logger)
        {
            _logger = logger;
        }
        public  async Task SeedAsync(UserManager<IdentityUser> userManager,ApplicationDbContext context)
        {
            _logger.LogInformation("start");
           
          
            if (userManager.Users.All(u => u.UserName != "admin@mail.com"))
            {
                var user = new IdentityUser
                {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                   
                };

                var result = await userManager.CreateAsync(user, "Admin@1234");

               
            }

            if (userManager.Users.All(u => u.UserName != "user@mail.com"))
            {
                var user = new IdentityUser
                {
                    UserName = "user@mail.com",
                    Email = "user@mail.com",
                    EmailConfirmed = true,
                   
                };

                var result = await userManager.CreateAsync(user, "User@1234");

              
            }

            if (context.MoneyTransfers.Any())
            {
                
                return;
            }

            var transfers = new[]
            {
            new MoneyTransferModel
            {
                SenderFirstName = "Ram",
                SenderLastName = "Bahadur",
                ReceiverFirstName = "Evan",
                ReceiverLastName = "Dangol",
                BankName = "Nabil Bank ",
                AccountNumber = "1234567890",
                TransferAmountMYR = 1000.00m,
                 ExchangeRate = 31.00m,
                PayoutAmountNPR = 31000.00m,
                TransferDate = DateTime.UtcNow
            },
            new MoneyTransferModel
            {
                SenderFirstName = "Shyam",
                SenderLastName = "Bagadur",
                ReceiverFirstName = "Hari",
                ReceiverLastName = "Bahadur",
                BankName = "Global Bank",
                AccountNumber = "0987654321",
                TransferAmountMYR = 2000.00m,
                ExchangeRate = 31.00m,
                PayoutAmountNPR = 62000.00m,
                TransferDate = DateTime.UtcNow
            },
           
        };
            _logger.LogInformation("end");

            await context.MoneyTransfers.AddRangeAsync(transfers);
          var x=  await context.SaveChangesAsync();
            _logger.LogInformation($"{x.ToString()}");
        }
    }

}
