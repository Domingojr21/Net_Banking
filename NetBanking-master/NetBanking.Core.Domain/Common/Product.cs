

namespace NetBanking.Core.Domain.Common
{
    public abstract class Product : AuditableEntity
    {
        public string IdentifyingNumber { get; set; } = null!;
        public string UserNameofOwner { get; set; } = null!;



    }
}
