using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Domain.Entities
{
    public class Beneficiary : AuditableEntity
    {
        public string IdentifyingNumberofProduct { get; set; } = null!;
        public string BeneficiaryUserName { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
