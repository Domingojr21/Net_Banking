using NetBanking.Core.Application.Dictionary;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Entities;
using NetBanking.Infrastructure.Persistence.Context;


namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class SavingAccountRepository : GenericRepository<SavingAccount>, ISavingAccountRepository
    {
        public SavingAccountRepository(NetBankingContext dbContext) : base(dbContext)
        {
        }

        public override async Task<SavingAccount> AddAsync(SavingAccount entity)
        {
            var list = await GetAllAsync();

            entity.IdentifyingNumber = ProductNumberGenerator.AlgorithmForProductNumber<SavingAccount>(ProductPrefixis.ProductPrefixDictionary["SavingAccount"], list);

            return await base.AddAsync(entity);
        }

        public async Task<SavingAccount> GetSavingAccountByOwner(string ownerUserName)
        {
            var allAccounts = await GetAllAsync();
            return allAccounts.FirstOrDefault(sa => sa.UserNameofOwner == ownerUserName);
        }


    }
}
