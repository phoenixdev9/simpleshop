using api.Extensions;
using api.Middleware;
using api.Utils;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            _Configuration = configuration;
        }

        private readonly IConfiguration _Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();
            //Add Auto Mapper
            services.AddAutoMapper (typeof (AutoMapping));
            //Add DB
            services.AddDbContext<StoreContext> (x =>
                x.UseSqlite (_Configuration.GetConnectionString ("DefaultConnection")));
            //Add Extension for dependency injections
            services.AddApplicationServices ();
            //Add Cors
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ()
                    .WithOrigins ("http://localhost:4200");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            app.UseMiddleware<ExceptionMiddleware> ();
            app.UseStatusCodePagesWithReExecute ("/errors/{0}");
            app.UseHttpsRedirection ();
            app.UseRouting ();
            app.UseCors ("CorsPolicy");
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}