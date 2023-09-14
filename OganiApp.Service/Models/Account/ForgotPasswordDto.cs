using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Models.Account
{
    public class ForgotPasswordDto
    {
        [EmailAddress(ErrorMessage = "*E-mail formatinda('@') yazi daxil edin.")]
        public string? Email { get; set; }
    }
}
