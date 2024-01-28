

using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Interfaces.Repositories
{
    public interface IBeneficiaryRepository : IGenericRepository<Beneficiary>
    {
        Task<bool> AddBeneficiary(string identifyingNumber);
        Task<List<Beneficiary>> GetBeneficiryByUserId(string idUser);
    }
}
