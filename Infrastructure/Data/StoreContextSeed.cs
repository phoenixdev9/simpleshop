using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
// using Core.Entities;
// using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data {
    public class StoreContextSeed {
        public static async Task SeedAsync (StoreContext context, ILoggerFactory loggerFactory, UserManager<User> userManager) {
            var path = "../Infrastructure/Data/SeedData/";
            try {

                if (!context.ProductBrands.Any ()) {
                    var brandsData = System.IO.File.ReadAllText (path + "brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>> (brandsData);

                    foreach (var item in brands) {
                        context.ProductBrands.Add (item);
                    }

                    await context.SaveChangesAsync ();
                }

                if (!context.ProductTypes.Any ()) {

                    var typesData = System.IO.File.ReadAllText (path + "types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>> (typesData);

                    foreach (var item in types) {
                        context.ProductTypes.Add (item);
                    }
                    await context.SaveChangesAsync ();
                }

                if (!context.Products.Any ()) {
                    var productsData = System.IO.File.ReadAllText (path + "products.json");
                    var products = JsonSerializer.Deserialize<List<Product>> (productsData);

                    foreach (var item in products) {
                        context.Products.Add (item);
                    }

                    await context.SaveChangesAsync ();
                }
                if (!userManager.Users.Any ()) {
                    var user = new User {
                        DisplayName = "Eric",
                        Email = "Eric@email.com",
                        UserName = "Eric@email.com",
                        address = new Address {
                        Name = "Eric",
                        Street = "104st 41a ave",
                        City = "Vancouver",
                        Province = "BC",
                        PostalCode = "V1N4L3"
                        }
                    };

                    await userManager.CreateAsync (user, "Pa$$w0rd");
                }
            } catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<StoreContextSeed> ();
                logger.LogError (ex.Message);
            }
        }
    }
}