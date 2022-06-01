using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Cqrs.Implementations;
using System.Reflection;

namespace QuokkaDev.Cqrs
{
    /// <summary>
    /// Extensions method for dependency injection registration
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the CQRS infrastructure.
        /// Register all the queries, the commands and the handlers in a given set of assemblies
        /// </summary>
        /// <param name="services">The service collection where register the CQRS</param>
        /// <param name="assemblies">An array of assemblies to scan for queries, commands and handlers registration</param>
        /// <returns>The service collection, so you can chain multiple methods</returns>
        public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
        {
            if(assemblies is null || assemblies.Length == 0) {
                assemblies = new Assembly[] { Assembly.GetCallingAssembly() };
            }

            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.Scan(selector => {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter => {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });

            services.Scan(selector => {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter => {
                            filter.AssignableTo(typeof(ICommandHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });

            return services;
        }
    }
}
