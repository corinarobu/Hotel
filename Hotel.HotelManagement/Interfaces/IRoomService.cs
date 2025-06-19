using Hotel.HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Interfaces
{
    public interface IRoomService
    {
        Task<RoomGetDTO> AddRoomAsync(RoomAddDTO roomDto);
        Task<RoomGetDTO> UpdateRoomAsync(int id_Room, RoomAddDTO roomDto);
        Task<bool> DeleteRoomAsync(int id_Room);
        Task<RoomGetDTO> GetRoomByIdAsync(int id_Room);
        Task<IEnumerable<RoomGetDTO>> GetAllRoomsAsync();
        Task<IEnumerable<RoomGetDTO>> FilteredRoomsAsync(int? capacity,decimal? minPrice,decimal? maxPrice,bool? isAvailable,int? viewType,bool? hasBreakfastIncluded,int? mealPlan);
    }
}
