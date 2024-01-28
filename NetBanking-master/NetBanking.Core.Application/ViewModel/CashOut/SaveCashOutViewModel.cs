using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.AvancedeEfectivo
{
    public class SaveCashOutViewModel
    {
        public string? IdUser {  get; set; }
        [Required(ErrorMessage = "Debe elegir la Tarjeta de Credito de donde pagara")]
        [Range(1, double.MaxValue, ErrorMessage = "El Numero de Cuenta debe ser Seleccionado")]
        public string CardNumber {  get; set; }
        [Required(ErrorMessage = "Debe elegir la cuenta ahorro a la que le hará el avance de efectivo")]
        [Range(1, double.MaxValue, ErrorMessage = "El Numero de Cuenta debe ser Seleccionado")]
        public string DestinationAccount {  get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "0 no es un monto Valido")]
        public decimal Amount {  get; set; }

        public bool HasError {  get; set; }
        public string? Error {  get; set; }
    }
}
