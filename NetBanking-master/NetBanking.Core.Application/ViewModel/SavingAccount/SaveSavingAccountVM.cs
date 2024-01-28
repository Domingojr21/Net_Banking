

using NetBanking.Core.Application.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.SavingAccount
{
    public class SaveSavingAccountVM
    {
        public int Id { get; set; }
        public string? IdentifyingNumber { get; set; }
        [Required(ErrorMessage = "Debe elegir al usuario al que le creara la cuenta")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El número de cuenta debe ser seleccionado")]
        public string UserNameofOwner { get; set; }
        public bool IsPrincipal { get; set; } = false;
        [Range(1, double.MaxValue, ErrorMessage = "0 no es un monto Valido")]
        public decimal Amount { get; set; }
        public List<UserDTO>? users { get; set; }
        
    }
}
