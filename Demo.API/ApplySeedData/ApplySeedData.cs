using Core.Context;
using Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.ApplySeedData
{
    public class ApplySeedData
    {
        public static async Task ApplySeedDataAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreAppDb>();
                    var identityContext = services.GetRequiredService<IdentityAppDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await StoreSeedData.SeedDataAsync(context, loggerFactory);
                    await AppIdentitySeedData.SeedUserAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<StoreAppDb>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
