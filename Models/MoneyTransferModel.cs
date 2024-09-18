using System.ComponentModel.DataAnnotations;

namespace AbcMoneyTransfer.Models
{
    public class MoneyTransferModel
    {
        [Key]
        public int Id { get; set; } // Primary key

        public string? SenderFirstName { get; set; }
        public string? SenderLastName { get; set; }
        public string? ReceiverFirstName { get; set; }
        public string? ReceiverLastName { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "The transfer amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The transfer amount must be greater than 0.")]
        public decimal TransferAmountMYR { get; set; }

        public decimal ExchangeRate { get; set; }
        public decimal PayoutAmountNPR { get; set; }

        public DateTime TransferDate { get; set; } = DateTime.Now; 
    }

}
