namespace QuokkaDev.Cqrs.Abstractions
{
    /// <summary>
    /// Interface for command dispatcher
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatch a command to the right handler
        /// </summary>
        /// <typeparam name="TCommand">Type of the command operation</typeparam>
        /// <typeparam name="TCommandResult">Type returned from the command operation</typeparam>
        /// <param name="command">The command operation</param>
        /// <param name="cancellation">A cancellation token</param>
        /// <returns>The response to the command from the query handler</returns>
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);
        /// <summary>
        /// Dispatch a command to the right handler
        /// </summary>
        /// <typeparam name="TCommand">Type of the command operation</typeparam>
        /// <typeparam name="TCommandResult">Type returned from the command operation</typeparam>
        /// <param name="command">The command operation</param>
        /// <returns>The response to the command from the query handler</returns>
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command);
    }
}
