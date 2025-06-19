using Hotel.HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Interfaces
{
    public interface IFAQService
    {
        Task<FAQGetDTO> AddFAQAsync(FAQAddDTO faqDto);
        Task<FAQGetDTO> UpdateFAQAsync(int id_FAQ, FAQAddDTO faqDto);
        Task<bool> DeleteFAQAsync(int id_FAQ);
        Task<FAQGetDTO> GetFAQByIdAsync(int id_FAQ);
        Task<List<FAQGetDTO>> GetAllFAQs();

    }
}
