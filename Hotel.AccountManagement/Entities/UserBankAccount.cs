using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Entities
{
    public class UserBankAccount 
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive amount.")]
        public decimal Balance {  get; set; }
        [Required]
        public string IBAN { get; set; } = string.Empty;
        [Required]
        public string BankName { get; set; } = string.Empty ;
        [Required]
        public User User { get; set; }= new User();

    }
}
