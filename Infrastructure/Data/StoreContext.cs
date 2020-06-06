using Domains.Entities;
using Microsoft.EntityFrameworkCore;
//dotnet ef migrations add InitialCreate -o Data/Migrations
//dotnet ef database update
namespace Infrastructure.Data {
    public class StoreContext : DbContext {
        public StoreContext (DbContextOptions<StoreContext> options) : base (options) { }
        public DbSet<Product> Products { get; set; }

    }
}