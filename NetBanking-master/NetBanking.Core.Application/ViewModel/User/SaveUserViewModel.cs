using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
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

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "mira la contraseña para que funcione debe contener al menos un número y un carácter especial, una mayúscula y una minúscula")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
       public string ConfirmPassword { get; set; }       

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Debe de estar en formato de Republica Dominicana")]
        //[RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "El formato del teléfono de ser de Republica Dominica con 809, 829 o 849 -000 -0000")]
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Debe de colocar su cedula sin guiones")]
        [Required(ErrorMessage = "Debe ingresar su Cedula")]
        public string Cedula { get; set; } = null!;

        public decimal InitialAmount { get; set; }
    }
}
