using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.Entities;
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
    public class RecomandationService : IRecomandationService
    {
        private readonly HotelManagementDbContext _context;
        private readonly AccountManagementDbContext _accountContext;

        public RecomandationService(HotelManagementDbContext context, AccountManagementDbContext accountContext)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public async Task<RecomandationGetDTO> AddRecomandationAsync(RecomandationAddDTO recomandationDto,int userId)
        {
            _ = await _accountContext.Users.FindAsync(userId) ?? throw new Exception("No user found!");
            var recomandation = new Recomandation { 
                Name = recomandationDto.Name, 
                Description = recomandationDto.Description, 
                Address = recomandationDto.Address, 
                EntryFee = recomandationDto.EntryFee, 
                DistanceFromHotel = recomandationDto.DistanceFromHotel,
                UserId=userId
            };
            _context.Recomandations.Add(recomandation);

            await _context.SaveChangesAsync();

            return new RecomandationGetDTO { 
                Id_Recomandation = recomandation.Id_Recomandation, 
                Name = recomandation.Name, 
                Description = recomandation.Description,
                Address = recomandation.Address, 
                EntryFee = recomandation.EntryFee,
                DistanceFromHotel = recomandation.DistanceFromHotel,
                UserId = recomandation.UserId
            };
        }

        public async Task<bool> DeleteRecomandationAsync(int id_Recomandation, int userId)
        {
            var user = await _accountContext.Users.FindAsync(userId);
            if (user == null)
            {
                Console.WriteLine($"No user found with ID: {userId}");
                throw new Exception("No user found!");
            }

            var recomandation = await _context.Recomandations.FindAsync(id_Recomandation);
            if (recomandation == null)
            {
                Console.WriteLine($"Recommendation ID {id_Recomandation} not found.");
                return false;
            }

            bool isOwner = recomandation.UserId == userId;
            bool isAdmin = await _accountContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Join(_accountContext.Roles,
                      ur => ur.RoleId,
                      r => r.Id,
                      (ur, r) => r.Name)
                .AnyAsync(roleName => roleName == "Admin");

            if (!isOwner && !isAdmin)
            {
                Console.WriteLine($"User {userId} is not the owner and not an admin.");
                return false;
            }

            _context.Recomandations.Remove(recomandation);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<RecomandationGetDTO> UpdateRecomandationAsync(int id_Recomandation, RecomandationAddDTO recomandationDto)
        {
            var recomandation = await _context.Recomandations.FindAsync(id_Recomandation);
            if (recomandation == null)
            {
                return null;
            }

            recomandation.Name = recomandationDto.Name;
            recomandation.Description = recomandationDto.Description;
            recomandation.Address = recomandationDto.Address;
            recomandation.EntryFee = recomandationDto.EntryFee;
            recomandation.DistanceFromHotel= recomandationDto.DistanceFromHotel;

            await _context.SaveChangesAsync();

            return new RecomandationGetDTO
            {
                Id_Recomandation = recomandation.Id_Recomandation,
                Name = recomandation.Name,
                Description = recomandation.Description,
                Address= recomandation.Address,
                EntryFee = recomandation.EntryFee,
                DistanceFromHotel=recomandation.DistanceFromHotel,
            };
        }
        public async Task<RecomandationGetDTO> GetRecomandationByIdAsync(int id)
        {
            var recomandation = await _context.Recomandations.FindAsync(id);
            if (recomandation == null) return null;

            return new RecomandationGetDTO
            {
                Id_Recomandation = recomandation.Id_Recomandation,
                Name = recomandation.Name,
                Description = recomandation.Description,
                Address = recomandation.Address,
                EntryFee = recomandation.EntryFee,
                DistanceFromHotel = recomandation.DistanceFromHotel,
            };
        }
        public async Task<IEnumerable<RecomandationGetDTO>> GetAllRecomandationsAsync()
        {
            var recomandation = await _context.Recomandations.ToListAsync();
            return recomandation.Select(recomandation => new RecomandationGetDTO
            {
                Id_Recomandation = recomandation.Id_Recomandation,
                Name = recomandation.Name,
                Description = recomandation.Description,
                Address = recomandation.Address,
                EntryFee = recomandation.EntryFee,
                DistanceFromHotel = recomandation.DistanceFromHotel,
            });
        }
    }
}
