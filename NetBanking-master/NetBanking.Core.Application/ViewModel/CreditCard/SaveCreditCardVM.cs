using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.Transaction;
using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.CreditCard
{
    public class SaveCreditCardVM
    {
        public int Id { get; set; }

        public string? IdentifyingNumber { get; set; }
        [Required(ErrorMessage = "Debe elegir al usuario al que le creara la cuenta")]
        [Range(1, double.MaxValue, ErrorMessage = "El Numero de Cuenta debe ser Seleccionado")]
        public string UserNameofOwner { get; set; }
        [Required(ErrorMessage = "Es requerido")]
        public SaveTransactionVM? SaveTransactionVM { get; set; }
        public string? UserName {get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 1.")]
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
        public decimal CurrentAmount { get; set; }

        public List<UserDTO>? users;
    }
}
