using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MowaInfo.DbContextPool
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextPool<TContext>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 128)
            where TContext : DbContext
        {
            return Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContextPool<TContext>(serviceCollection, optionsAction, poolSize)
                .FixDbContextPool<TContext>();
        }

        public static IServiceCollection AddDbContextPool<TContext>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction,
            int poolSize = 128)
            where TContext : DbContext
        {
            return Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContextPool<TContext>(serviceCollection, optionsAction, poolSize)
                .FixDbContextPool<TContext>();
        }

        private static IServiceCollection FixDbContextPool<TContext>(this IServiceCollection serviceCollection)
            where TContext : DbContext
        {
            var origin = serviceCollection.Single(descriptor => descriptor.ServiceType == typeof(Microsoft.EntityFrameworkCore.Internal.DbContextPool<TContext>));
            var now = new ServiceDescriptor(origin.ServiceType, typeof(DbContextPool<TContext>), origin.Lifetime);
            serviceCollection.Replace(now);
            return serviceCollection;
        }
    }
}
