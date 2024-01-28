using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Domain.Entities
{
    public class SavingAccount : Product
    {
        public bool IsPrincipal { get; set; } = false;
        public decimal Amount { get; set; }
    }
}
