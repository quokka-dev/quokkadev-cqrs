using QuokkaDev.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace QuokkaDev.Cqrs.Implementations
{
    /// <summary>
    /// An implementation of ICommandDispatcher based on MediatR
    /// </summary>
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return handler.Handle(command, cancellation);
        }

        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command)
        {
            return Dispatch<TCommand, TCommandResult>(command, CancellationToken.None);
        }
    }
}
