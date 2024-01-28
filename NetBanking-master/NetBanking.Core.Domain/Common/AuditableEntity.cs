

namespace NetBanking.Core.Domain.Common
{
    public abstract class AuditableEntity : IsStatusEntity
    {
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool Status { get; set; } = true;
    }
}
