namespace QuokkaDev.Cqrs.Abstractions
{
    /// <summary>
    /// Interface for query dispatcher
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatch a query to the right handler
        /// </summary>
        /// <typeparam name="TQuery">Type of the query operation</typeparam>
        /// <typeparam name="TQueryResult">Type returned from the query operation</typeparam>
        /// <param name="query">The query operation</param>
        /// <param name="cancellation">A cancellation token</param>
        /// <returns>The response to the query from the query handler</returns>
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation);
        /// <summary>
        /// Dispatch a query to the right handler
        /// </summary>
        /// <typeparam name="TQuery">Type of the query operation</typeparam>
        /// <typeparam name="TQueryResult">Type returned from the query operation</typeparam>
        /// <param name="query">The query operation</param>
        /// <returns>The response to the query from the query handler</returns>
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query);
    }
}
