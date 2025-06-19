using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.DTOs
{
    public class RecomandationAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double EntryFee { get; set; }
        public double DistanceFromHotel { get; set; }
        public int UserId {  get; set; }
        public void setId(int id) => UserId = id;
    }
}
