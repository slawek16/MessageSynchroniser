using MessageSynchroniser.Application.Contracts.Persistence;
using MessageSynchroniser.Persistence.DatabaseContext;
using MessageSynchroniser.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageSynchroniser.Persistence
{
    public static class PersistentServiceSegregation
    {
        public static IServiceCollection AddPersistentServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MessageSynchroniserDatabaseContetx>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MessageSynchroniserDatabaseConnectionString"));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}
