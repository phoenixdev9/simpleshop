using System;
using System.Linq;
using System.Reflection;
using Domains.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//cd .. && dotnet ef migrations add FinishApp -p Infrastructure -s api -o Data/Migrations
//dotnet ef database update
namespace Infrastructure.Data {
    public class StoreContext : IdentityDbContext<User> {
        public StoreContext (DbContextOptions options) : base (options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly (Assembly.GetExecutingAssembly ());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite") {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
                    var properties = entityType.ClrType.GetProperties ().Where (p => p.PropertyType == typeof (decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties ()
                        .Where (p => p.PropertyType == typeof (DateTimeOffset));
                    foreach (var property in properties) {
                        modelBuilder.Entity (entityType.Name).Property (property.Name).HasConversion<double> ();
                    }
                    foreach (var property in dateTimeProperties) {
                        modelBuilder.Entity (entityType.Name).Property (property.Name)
                            .HasConversion (new DateTimeOffsetToBinaryConverter ());
                    }
                }
            }
        }
    }
}