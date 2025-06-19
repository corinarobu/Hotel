using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.DTOs
{
    public class FAQGetDTO
    {
        public int Id_FAQ { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public double Mark { get; set; }
        public int? IdRoom { get; set; }
    }
}
