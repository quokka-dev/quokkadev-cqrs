namespace QuokkaDev.Cqrs.Abstractions
{
    /// <summary>
    /// Generic query handler
    /// </summary>
    /// <typeparam name="TQuery">The type of query operation</typeparam>
    /// <typeparam name="TQueryResult">The type returned from the query operation</typeparam>
    public interface IQueryHandler<in TQuery, TQueryResult>
    {
        /// <summary>
        /// Handle the query
        /// </summary>
        /// <param name="query">The query to handle</param>
        /// <param name="cancellation">A cancellation token for long running tasks</param>
        /// <returns>The result of the query invocation</returns>
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
    }
}
