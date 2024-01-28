using NetBanking.Core.Application.Dictionary;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Entities;
using NetBanking.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(NetBankingContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Loan> AddAsync(Loan entity)
        {
            var list = await GetAllAsync();

            entity.IdentifyingNumber = ProductNumberGenerator.AlgorithmForProductNumber<Loan>(ProductPrefixis.ProductPrefixDictionary["Transactions"], list);

            return await base.AddAsync(entity);
        }

    }
}
