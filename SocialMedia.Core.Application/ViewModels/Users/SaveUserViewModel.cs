using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido de usuario")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar una contrasenia")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contrasenias no son iguales")]
        [Required(ErrorMessage = "Debe colocar una contrasenia")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar un email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un numero de telefono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+1-)?(809|829|849)-\d{3}-\d{4}$", ErrorMessage = "El número de teléfono no es válido en la República Dominicana")]
        public string? PhoneNumber { get; set; }

        public string? ProfilePhoto { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
