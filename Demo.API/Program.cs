using Core.Context;
using Demo.API.Extentions;
using Demo.API.Middlewares;
using Infrastructure.ApplySeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Demo.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreAppDb>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
            );
            
            builder.Services.AddDbContext<IdentityAppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyIdentityConnection"))
            );
            //Singlton : create one object Shared on the Application
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            builder.Services.AddApplicationServices();
            
            builder.Services.AddIdentityServices(builder.Configuration);
            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            //builder.Services.AddSwaggerGen(opt =>
            //{
            //    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            //    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Please enter token",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.Http,
            //        BearerFormat = "JWT",
            //        Scheme = "bearer"
            //    });

            //    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //            {
            //                Type=ReferenceType.SecurityScheme,
            //                Id="Bearer"
            //            }
            //    },
            //    new string[]{}
            //    }
            //});
            //});

            builder.Services.AddSwaggerserviceExtention();

            var app = builder.Build();

            await ApplySeedData.ApplySeedDataAsync(app);   

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseMiddleware<ExceptionMiddleware>();
            }
            app.UseStaticFiles(); 

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            }
    }
}