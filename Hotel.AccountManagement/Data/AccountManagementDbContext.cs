using Hotel.AccountManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccountManagement.Data
{
    public class AccountManagementDbContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options)
        {
        }

        public DbSet<UserBankAccount> AccountsUsers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Identity");

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Roles)
                .HasForeignKey(ur => ur.RoleId);
            modelBuilder.Entity<UserBankAccount>()
                .Property(b => b.Balance)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<UserBankAccount>()
                .HasOne(b=>b.User)
                .WithMany(u=>u.BankAccounts)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
           modelBuilder.Entity<Payment>()
                .HasOne(bo=>bo.BankAccount)
                .WithMany()
                .HasForeignKey(bo=>bo.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
                .HasOne(bo => bo.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(bo => bo.UserId)
                .OnDelete(DeleteBehavior.Restrict);
          
        }
    }
}
