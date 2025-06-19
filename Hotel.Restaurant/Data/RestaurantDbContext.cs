using Hotel.Restaurant.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meal");
                entity.HasKey(m => m.Meal_Id);
                entity.Property(m => m.Meal_Name)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(m=>m.Meal_Description)
                    .IsRequired()
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<Products>(entity => {
                entity.ToTable("Products");
                entity.HasKey(p=>p.Id_Product);
                entity.Property(p=>p.Name_Product)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(p => p.Type_Of_Product)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(p => p.Description_Product)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.HasOne(p=>p.Meal)
                    .WithMany(m=>m.Products)
                    .HasForeignKey(p=>p.Meal_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
