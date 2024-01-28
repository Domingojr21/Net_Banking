using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Application.Dictionary;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Entities;
using NetBanking.Infrastructure.Persistence.Context;

namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
    {
        private readonly NetBankingContext _netBankingContext;
        public CreditCardRepository(NetBankingContext dbContext, NetBankingContext netBankingContext) : base(dbContext)
        {
            _netBankingContext = netBankingContext;
        }

        public override async Task<CreditCard> AddAsync(CreditCard entity)
        {
            var list = await GetAllAsync();

            entity.IdentifyingNumber = ProductNumberGenerator.AlgorithmForProductNumber<CreditCard>(ProductPrefixis.ProductPrefixDictionary["CreditCards"], list);

            return await base.AddAsync(entity);
        }

        public async Task<CreditCard> GetByCardNumber(string cardNumber)
        {
            return await _netBankingContext.Credits.FirstOrDefaultAsync(x => x.IdentifyingNumber == cardNumber);
        }
    }
}
