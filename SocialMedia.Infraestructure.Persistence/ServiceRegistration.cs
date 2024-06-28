using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Infraestructure.Persistence.Context;
using SocialMedia.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("SocialMediaInMemory"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("SqlServerConnection");
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer(connectionString, a => a.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                });
            }
            #endregion

            #region Repositories
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IFriendRepository, FriendRepository>();

            #endregion
        }
    }
}
