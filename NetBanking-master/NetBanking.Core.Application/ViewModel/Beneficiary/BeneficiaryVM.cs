namespace NetBanking.Core.Application.ViewModel.Beneficiary
{
    public class BeneficiaryVM
    {
        public string IdentifyingNumberofProduct { get; set; } = null!;
        public string BeneficiaryUserName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Id { get; set; }
        public string? ErrorMessage { get; set; } 
    }
}
