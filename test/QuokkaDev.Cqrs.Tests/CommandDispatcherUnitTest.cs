using Moq;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Cqrs.Tests.Utilities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Cqrs.Tests
{
    public class CommandDispatcherUnitTest
    {
        private readonly Mock<ICommandHandler<TestCommand, TestCommandResult>> testCommandHandlerMock;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly DependencyInjectionContext context;

        public CommandDispatcherUnitTest()
        {
            context = new DependencyInjectionContext();

            testCommandHandlerMock = context.RegisterCommandHandler("My expected response");

            context.BuildServiceProvider();
            commandDispatcher = context.GetService<ICommandDispatcher>();
        }

        [Fact]
        public async Task Dispatch_A_Command_Should_Return_A_Not_Null_Response()
        {
            // Arrange
            var request = new TestCommand() { Message = "My request" };

            // Act
            var response = await commandDispatcher.Dispatch<TestCommand, TestCommandResult>(request);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Dispatch_A_Command_Should_Return_Expected_Response()
        {
            // Arrange
            var request = new TestCommand() { Message = "My request" };

            // Act
            var response = await commandDispatcher.Dispatch<TestCommand, TestCommandResult>(request);

            // Assert
            Assert.Equal("My expected response", response.Message);
        }

        [Fact]
        public async Task Dispatch_A_Command_Should_Call_IQueryHandler_Once()
        {
            // Arrange
            var request = new TestCommand() { Message = "My request" };

            // Act
            await commandDispatcher.Dispatch<TestCommand, TestCommandResult>(request);

            // Assert
            testCommandHandlerMock.Verify(handler => handler.Handle(It.IsAny<TestCommand>(), CancellationToken.None), Times.Exactly(1));
        }
    }
}
