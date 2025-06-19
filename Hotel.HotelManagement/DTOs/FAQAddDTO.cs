using Hotel.HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.DTOs
{
    public class FAQAddDTO
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public double Mark { get; set; }
        public int? IdRoom { get; set; }
    }
}
