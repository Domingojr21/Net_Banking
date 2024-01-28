using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public string OriginAccountNumber { get; set; } = null!;
        public string DestinationAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public string UserNameOfAccountHolder { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.Today;
        public string? Description { get; set; } 
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; } = null!;
    }
}
