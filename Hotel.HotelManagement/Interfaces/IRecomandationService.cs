using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Interfaces
{
    public interface IRecomandationService
    {
        Task<RecomandationGetDTO> AddRecomandationAsync(RecomandationAddDTO recomandationDto,int userId);
        Task<RecomandationGetDTO> UpdateRecomandationAsync(int id_Recomandation, RecomandationAddDTO recomandationDto);
        Task<bool> DeleteRecomandationAsync(int id_Recomandation, int userId);
        Task<RecomandationGetDTO> GetRecomandationByIdAsync(int id_Recomandation);
        Task<IEnumerable<RecomandationGetDTO>> GetAllRecomandationsAsync();
    }
}
