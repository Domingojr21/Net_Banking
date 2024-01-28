
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanVM, LoanVM, Loan>
    {
        Task<List<LoanVM>> GetAllVMbyUserId();
        Task<LoanVM> GetByLoanINumber(string identifyingNumber);
    }
}
