

using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.Transaction
{
    public class SaveTransactionVM
    {
        [Required(ErrorMessage = "Debe elegir una Cuenta de Origen")]
        [Range(1, double.MaxValue, ErrorMessage = "El Numero de Cuenta debe ser Seleccionado")]
        public string OriginAccountNumber { get; set; } = null!;
        [Required(ErrorMessage = "El número de cuenta es requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de cuenta debe tener 9 dígitos")]
        public string DestinationAccountNumber { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 1.")]
        public decimal Amount { get; set; }
        public string? UserNameOfAccountHolder { get; set; } = null!;
        public DateTime? Date { get; set; } = DateTime.Today;
        public string? Description { get; set; } = null!;
        public int? TransactionTypeId { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public bool HasError { get; set; } = false; 
        public string? ErrorMessage { get; set; }
    }
}
