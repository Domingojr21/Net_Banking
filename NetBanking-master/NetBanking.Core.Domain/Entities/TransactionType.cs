using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Domain.Entities
{
    public class TransactionType : AuditableEntity
    {
        public string TransactionTypeName {get; set;} = null!;
        public ICollection<Transaction>? Transactions {get; set;}
    }
}
