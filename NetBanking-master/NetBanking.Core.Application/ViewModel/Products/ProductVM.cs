using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.SavingAccount;


namespace NetBanking.Core.Application.ViewModel.Products
{
    public class ProductVM
    {
        public List<LoanVM>? LoanVMs { get; set; }
        public List<SavingAccountVM>? SavingAccounts { get; set; }
        public List<CreditCardVM>? CreditCards { get; set; }
    }
}
