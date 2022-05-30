using QuokkaDev.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace QuokkaDev.Cqrs.Implementations
{
    /// <summary>
    /// An implementation of IQueryDispatcher based on MediatR
    /// </summary>
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            return handler.Handle(query, cancellation);
        }

        public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query)
        {
            return Dispatch<TQuery, TQueryResult>(query, CancellationToken.None);
        }
    }
}
