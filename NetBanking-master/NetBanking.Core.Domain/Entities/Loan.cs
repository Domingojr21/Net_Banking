using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Domain.Entities
{
    public class Loan : Product
    {
        public decimal LoanQuantity { get; set; }
        public decimal PaidQuantity { get; set; }

    }
}
