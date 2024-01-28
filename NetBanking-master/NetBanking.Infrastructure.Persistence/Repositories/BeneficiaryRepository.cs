
using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Entities;
using NetBanking.Infrastructure.Persistence.Context;

namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class BeneficiaryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly NetBankingContext _dbContext;
        public BeneficiaryRepository(NetBankingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddBeneficiary(string identifyingNumber)
        {
            return true;            
        }

        public async Task<List<Beneficiary>> GetBeneficiryByUserId(string idUser)
        {
            return await _dbContext.Beneficiaries.Where(X => X.UserName == idUser).ToListAsync();
        }
    }
}
