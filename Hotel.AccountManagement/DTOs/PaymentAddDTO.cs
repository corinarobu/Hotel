using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.DTOs
{
    public class PaymentAddDTO
    {
        public int Id {  get; set; }
        public double TotalPrice {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

    }
}
