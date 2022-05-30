namespace QuokkaDev.Cqrs.Abstractions
{
    /// <summary>
    /// Generic command handler
    /// </summary>
    /// <typeparam name="TCommand">The type of command operation</typeparam>
    /// <typeparam name="TCommandResult">The type returned from the command operation</typeparam>
    public interface ICommandHandler<in TCommand, TCommandResult>
    {
        /// <summary>
        /// Handle the command
        /// </summary>
        /// <param name="command">The command to handle</param>
        /// <param name="cancellation">A cancellation token for long running tasks</param>
        /// <returns>The result of the command invocation</returns>
        Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
    }
}
