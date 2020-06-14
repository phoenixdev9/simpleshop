using System.Linq;
using api.Error;
using Domains.IRepo;
using Domains.IServices;
using Domains.Repo;
using Infrastructure.Data.Repo;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped(typeof(IGenericsRepo<>), (typeof(GenericsRepo<>)));
            services.AddScoped<IToken, Token>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IBasketRepo, BasketRepo>();

            //Error Validation
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ErrorValidation
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}