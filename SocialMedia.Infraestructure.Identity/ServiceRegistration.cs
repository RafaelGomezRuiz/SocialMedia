using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Infraestructure.Identity.Context;
using SocialMedia.Infraestructure.Identity.Entities;
using SocialMedia.Infraestructure.Identity.Seeds;
using SocialMedia.Infraestructure.Identity.Services;

namespace SocialMedia.Infraestructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfraestruture(this IServiceCollection services,IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseInMemoryDatabase("IdentityContextInMemory");
                });
            }
            else
            {
                var connectionString = configuration.GetConnectionString("IdentityConnection");
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlServer(connectionString,a=>a.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

        public static async Task AddIdentitySeeds(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try{
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await DefaultRoles.SeedAsync(roleManager);
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
