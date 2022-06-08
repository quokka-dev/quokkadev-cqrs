using FluentAssertions;
using Moq;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Cqrs.Tests.Utilities;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Cqrs.Tests
{
    public class ServiceCollectionUnitTest
    {
        public ServiceCollectionUnitTest()
        {
        }

        [Fact]
        public async Task Registration_With_Empty_Assembly_Should_Work_With_Executing_Assembly()
        {
            // Arrange
            DependencyInjectionContext context = new DependencyInjectionContext();
            Mock<ICommandHandler<TestCommand, TestCommandResult>> testCommandHandlerMock = context.RegisterCommandHandler("My expected response");
            Mock<IQueryHandler<TestQuery, TestQueryResult>> testQueryHandlerMock = context.RegisterQueryHandler("My expected response");

            context.BuildServiceProvider();

            // Act
            var commadHandler = context.GetService<ICommandHandler<TestCommand, TestCommandResult>>();
            var queryHandler = context.GetService<IQueryHandler<TestQuery, TestQueryResult>>();

            // Assert
            commadHandler.Should().NotBeNull("command handler in test assembly must be used");
            queryHandler.Should().NotBeNull("query handler in test assembly must be used");

            commadHandler.Should().Be(testCommandHandlerMock.Object, "mock command handler must be used");
            queryHandler.Should().Be(testQueryHandlerMock.Object, "mock query handler must be used");
        }
    }
}
