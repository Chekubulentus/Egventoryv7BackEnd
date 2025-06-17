using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Services;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.DataContext
{
    public class PoultryContext : DbContext
    {
        public PoultryContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Account>()
                .HasOne(a => a.Employee)
                .WithOne(e => e.Account)
                .HasForeignKey<Account>(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Sales)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Price)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .IsRequired();

                entity.HasOne(e => e.Sales)
                      .WithMany(s => s.SalesItems)
                      .HasForeignKey(e => e.SalesId)
                      .OnDelete(DeleteBehavior.Cascade);

            });
            base.OnModelCreating(modelBuilder);

        }
    }
}
