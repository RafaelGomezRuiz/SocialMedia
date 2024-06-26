using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Infraestructure.Identity.Context;
using SocialMedia.Infraestructure.Identity.Entities;
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
    }
}
