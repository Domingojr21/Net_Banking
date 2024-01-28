using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.User
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe de estar en formato de Republica Dominicana")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "El formato del teléfono de ser de Republica Dominica con 809, 829 o 849 -000 -0000")]
        public string Phone { get; set; }
        public List<string> Roles { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Debe de colocar su cedula sin guiones")]
        [Required(ErrorMessage = "Debe ingresar su Cedula")]
        public string Cedula { get; set; } = null!;

        public decimal InitialAmount { get; set; } 
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
