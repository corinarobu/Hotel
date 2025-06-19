using Hotel.HotelManagement.Data;
using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Entities;
using Hotel.HotelManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelManagementDbContext _context;

        public RoomService(HotelManagementDbContext context)
        {
            _context = context;
        }
        public async Task<RoomGetDTO> AddRoomAsync(RoomAddDTO roomDto)
        {
            var room = new Room {RoomNumber=roomDto.RoomNumber, Description = roomDto.Description, Capacity = roomDto.Capacity, PricePerNight = roomDto.PricePerNight, IsAvailable=roomDto.IsAvailable, ViewType=roomDto.ViewType, HasBreakfastIncluded = roomDto.HasBreakfastIncluded,MealPlan=roomDto.MealPlan,startDate=roomDto.StartDate,endDate=roomDto.EndDate};
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return new RoomGetDTO { Id_Room=room.Id_Room,RoomNumber=room.RoomNumber,Description = room.Description, Capacity = room.Capacity, PricePerNight = room.PricePerNight, IsAvailable = room.IsAvailable, ViewType = room.ViewType, HasBreakfastIncluded = room.HasBreakfastIncluded, MealPlan = room.MealPlan,StartDate=room.startDate,EndDate=room.endDate,FAQs=room.FAQs };
        }

        public async Task<bool> DeleteRoomAsync(int id_Room)
        {
            var ingredient = await _context.Rooms.FindAsync(id_Room);
            if (ingredient == null) { return false; }
            _context.Rooms.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<RoomGetDTO> UpdateRoomAsync(int id_Room, RoomAddDTO roomDto)
        {
            var room = await _context.Rooms.FindAsync(id_Room);
            if (room == null)
            {
                return null;
            }
            room.RoomNumber = roomDto.RoomNumber;
            room.Description = roomDto.Description;
            room.Capacity = roomDto.Capacity;   
            room.PricePerNight = roomDto.PricePerNight;
            room.IsAvailable = roomDto.IsAvailable;
            room.ViewType = roomDto.ViewType;
            room.HasBreakfastIncluded = roomDto.HasBreakfastIncluded;
            room.MealPlan = roomDto.MealPlan;
            room.startDate = roomDto.StartDate;
            room.endDate = roomDto.EndDate;
           
            await _context.SaveChangesAsync();

            return new RoomGetDTO
            {
                Id_Room = room.Id_Room,
                RoomNumber = room.Id_Room,
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                ViewType = room.ViewType,
                HasBreakfastIncluded = room.HasBreakfastIncluded,
                MealPlan = room.MealPlan,
                StartDate=room.startDate,
                EndDate=room.endDate,
                FAQs= room.FAQs
            };
        }
        public async Task<RoomGetDTO> GetRoomByIdAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return null;

            return new RoomGetDTO
            {
                Id_Room = room.Id_Room,
                RoomNumber = room.Id_Room,
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                ViewType = room.ViewType,
                HasBreakfastIncluded = room.HasBreakfastIncluded,
                MealPlan = room.MealPlan,
                StartDate = room.startDate,
                EndDate = room.endDate,

            };
        }
        public async Task<IEnumerable<RoomGetDTO>> GetAllRoomsAsync()
        {
            var rooms = await _context.Rooms.ToListAsync();

            return rooms.Select(room => new RoomGetDTO
            {
                Id_Room = room.Id_Room,
                RoomNumber = room.Id_Room,
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                ViewType = room.ViewType,
                HasBreakfastIncluded = room.HasBreakfastIncluded,
                MealPlan = room.MealPlan,
                StartDate = room.startDate,
                EndDate = room.endDate,
                FAQs=room.FAQs
            });
        }
        public async Task<IEnumerable<RoomGetDTO>> FilteredRoomsAsync(
    int? capacity,
    decimal? minPrice,
    decimal? maxPrice,
    bool? isAvailable,
    int? viewType,
    bool? hasBreakfastIncluded,
    int? mealPlan)
        {
            var query = _context.Rooms.AsQueryable();

            if (capacity.HasValue)
                query = query.Where(r => r.Capacity == capacity.Value);

            if (minPrice.HasValue)
                query = query.Where(r => r.PricePerNight >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(r => r.PricePerNight <= maxPrice.Value);

            if (isAvailable.HasValue)
                query = query.Where(r => r.IsAvailable == isAvailable.Value);

            if (viewType.HasValue)
                query = query.Where(r => (int)r.ViewType == viewType.Value);

            if (hasBreakfastIncluded.HasValue)
                query = query.Where(r => r.HasBreakfastIncluded == hasBreakfastIncluded.Value);

            if (mealPlan.HasValue)
                query = query.Where(r => (int)r.MealPlan == mealPlan.Value);

            var rooms = await query.ToListAsync();

            return rooms.Select(room => new RoomGetDTO
            {
                Id_Room = room.Id_Room,
                RoomNumber = room.RoomNumber,
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                ViewType = room.ViewType,
                HasBreakfastIncluded = room.HasBreakfastIncluded,
                MealPlan = room.MealPlan,
                StartDate = room.startDate,
                EndDate = room.endDate,
                FAQs=room.FAQs
            });
        }
    }
}
