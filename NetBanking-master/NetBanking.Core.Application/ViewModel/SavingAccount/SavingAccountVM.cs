

namespace NetBanking.Core.Application.ViewModel.SavingAccount
{
    public class SavingAccountVM
    {
        public int Id { get; set; } 
        public string IdentifyingNumber { get; set; } 
        public string UserName { get; set; }
        public string UserNameofOwner { get; set; }
        public bool IsPrincipal { get; set; }
        public decimal Amount { get; set; }
    }
}
