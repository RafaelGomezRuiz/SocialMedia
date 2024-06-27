using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Users
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Debe colocar su nombre de usuario")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
