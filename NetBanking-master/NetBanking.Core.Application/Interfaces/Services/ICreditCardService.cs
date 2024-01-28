using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<SaveCreditCardVM, CreditCardVM, CreditCard>
    {
        Task<List<CreditCardVM>> GetAllVMbyUserId();

        Task<SaveCreditCardVM> GetByCardNumber(string cardNumber);
        Task<CreditCardVM> GetByCardIdentifyinNumber(string identifyingNumber);
    }
}
