using Hotel.HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Data
{
    public class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Recomandation> Recomandations { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Rating> FaqRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
                entity.HasKey(r => r.Id_Room);

                entity.Property(r => r.RoomNumber)
                   .HasColumnType("integer");

                entity.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(r => r.Capacity)
                    .HasColumnType("integer");

                entity.Property(r => r.PricePerNight)
                    .HasColumnType("decimal(18,2)");

                entity.Property(r => r.IsAvailable)
                    .HasColumnType("bit");

                entity.Property(r => r.ViewType)
                    .HasConversion<int>();

                entity.Property(r => r.HasBreakfastIncluded)
                    .HasColumnType("bit");

                entity.Property(r => r.MealPlan)
                    .HasConversion<int>();
            });

            modelBuilder.Entity<FAQ>(entity =>
            {
                entity.ToTable("FAQ");
                entity.HasKey(f => f.Id_FAQ);

                entity.Property(f => f.Question)
                    .HasMaxLength(200);

                entity.Property(f => f.Answer)
                    .HasMaxLength(200);

                entity.HasOne(f => f.Room)
                    .WithMany(r => r.FAQs)
                    .HasForeignKey(f => f.IdRoom)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Recomandation>(entity =>
            {
                entity.ToTable("Recomandation");
                entity.HasKey(re => re.Id_Recomandation);

                entity.Property(re => re.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(re => re.Description)
                    .HasMaxLength(200);

                entity.Property(re => re.Address)
                    .HasMaxLength(200);

                entity.Property(re => re.EntryFee)
                    .HasColumnType("decimal(18,2)");

                entity.Property(re => re.DistanceFromHotel)
                    .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("FaqRating");
                entity.HasKey(fr => fr.Id);

                entity.Property(fr => fr.Score).HasColumnType("integer");
                entity.Property(fr => fr.CreatedAt).HasColumnType("datetime");

               
            });

        }

    }
}
