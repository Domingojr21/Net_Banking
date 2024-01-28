using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Interfaces.Repositories
{
    public interface ICreditCardRepository : IGenericRepository<CreditCard>
    {
        Task<CreditCard> GetByCardNumber(string cardNumber);
    }
}
