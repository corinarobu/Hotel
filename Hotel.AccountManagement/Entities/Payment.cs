using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Amount must be greater than zero!")]
        public double TotalPrice {  get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [Required]
        public int UserId {  get; set; }
        [Required]
        public User User { get; set; } = new User();
        public int BankAccountId {  get; set; }
        public UserBankAccount BankAccount { get; set; } = new UserBankAccount();
    }
}
