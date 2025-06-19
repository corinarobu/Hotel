using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100,ErrorMessage = " Full name can't be longer then 100 characters!")]
        public string FullName {  get; set; } = string.Empty;
        [Required]
        public string Email {  get; set; }  =string.Empty;
        [Required]
        [StringLength(20,MinimumLength =8,ErrorMessage ="Password must be between 8 and 20 charaters")]
        public string Password { get; set; }=string.Empty;
    }
}
