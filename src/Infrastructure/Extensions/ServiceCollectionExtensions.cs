using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Application.Interfaces.Services.Storage;
using DancePlatform.Application.Interfaces.Services.Storage.Provider;
using DancePlatform.Application.Interfaces.Serialization.Serializers;
using DancePlatform.Application.Serialization.JsonConverters;
using DancePlatform.Infrastructure.Repositories;
using DancePlatform.Infrastructure.Services.Storage;
using DancePlatform.Application.Serialization.Options;
using DancePlatform.Infrastructure.Services.Storage.Provider;
using DancePlatform.Application.Serialization.Serializers;

namespace DancePlatform.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}