using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NetBanking.Core.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Domain.Entities.Transaction>
    {
    }
}
