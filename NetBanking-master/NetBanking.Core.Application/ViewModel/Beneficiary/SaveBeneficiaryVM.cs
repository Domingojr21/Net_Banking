using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.Beneficiary
{
    public class SaveBeneficiaryVM
    {
        public string? BeneficiaryUserName { get; set; } 
        public string? UserName { get; set; } 

        [Required(ErrorMessage = "El número de cuenta es requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de cuenta debe tener 9 dígitos")]
        public string IdentifyingNumberofProduct { get; set; }
        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; } 
    }
}
