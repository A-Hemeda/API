using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices.Dto;
using Services.ProductServices;
using Demo.API.HandleResponse;
using Services.CacheServices;
using Infrastructure.BasketRepository;
using Services.BasketServices;
using Services.TokenServices;
using Services.UserServices;
using Services.OrderServices.Dto;
using Services.OrderServices;
using Services.PaymentServices;

namespace Demo.API.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductservices, Productservices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionResult =>
                {
                    var errors = actionResult.ModelState
                                             .Where(M => M.Value.Errors.Count > 0)
                                             .SelectMany(M => M.Value.Errors)
                                             .Select(error => error.ErrorMessage).ToList();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            }

            );
            return services;
        }
        
    }
}
