using Hotel.HotelManagement.Data;
using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Entities;
using Hotel.HotelManagement.Interfaces;
using Hotel.BookManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Services
{
    public class FAQService : IFAQService
    {
        private readonly HotelManagementDbContext _context;
        private readonly IBookService _bookService;

        public FAQService(HotelManagementDbContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public async Task<FAQGetDTO> AddFAQAsync(FAQAddDTO faqDto)
        {
            var faq = new FAQ
            {
                Question = faqDto.Question,
                Answer = faqDto.Answer,
                IdRoom = faqDto.IdRoom
            };

            _context.FAQs.Add(faq);
            await _context.SaveChangesAsync();

            var mark = faqDto.Mark;

            return new FAQGetDTO
            {
                Id_FAQ = faq.Id_FAQ,
                Question = faq.Question,
                Answer = faq.Answer,
                Mark = mark,
                IdRoom = faq.IdRoom
            };
        }


        public async Task<bool> DeleteFAQAsync(int id_FAQ)
        {
            var faq = await _context.FAQs.FindAsync(id_FAQ);
            if (faq == null) return false;
            _context.FAQs.Remove(faq);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FAQGetDTO> UpdateFAQAsync(int id_FAQ, FAQAddDTO faqDto)
        {
            var faq = await _context.FAQs.FindAsync(id_FAQ);
            if (faq == null) return null;

            faq.Question = faqDto.Question;
            faq.Answer = faqDto.Answer;
            faq.IdRoom = faqDto.IdRoom;

            await _context.SaveChangesAsync();

            return new FAQGetDTO
            {
                Id_FAQ = faq.Id_FAQ,
                Question = faq.Question,
                Answer = faq.Answer,
                IdRoom = faq.IdRoom
            };
        }

        public async Task<FAQGetDTO> GetFAQByIdAsync(int id)
        {
            var faq = await _context.FAQs.FindAsync(id);
            if (faq == null) return null;

            return new FAQGetDTO
            {
                Id_FAQ = faq.Id_FAQ,
                Question = faq.Question,
                Answer = faq.Answer,
                IdRoom = faq.IdRoom
            };
        }

        public async Task<List<FAQGetDTO>> GetAllFAQs()
        {
            var faqs = await _context.FAQs.ToListAsync();
            return faqs.Select(faq => new FAQGetDTO
            {
                Id_FAQ = faq.Id_FAQ,
                Question = faq.Question,
                Answer = faq.Answer,
                IdRoom = faq.IdRoom
            }).ToList();
        }

      
    }
}
