using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Models.Account
{
	public class UserUpdateDto
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		public string? UserName { get; set; }

		public string? CurrentPassword { get; set; }


		public string? NewPassword { get; set; }


		[Compare("NewPassword")]
		public string? ConfirmNewPassword { get; set; }
	}
}
