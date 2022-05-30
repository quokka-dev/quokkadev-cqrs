using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Cqrs.Tests.Utilities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Cqrs.Tests;

public class QueryDispatcherUnitTest
{
    private readonly Mock<IQueryHandler<TestQuery, TestQueryResult>> testQueryHandlerMock;
    private readonly IQueryDispatcher queryDispatcher;
    private readonly DependencyInjectionContext context;

    public QueryDispatcherUnitTest()
    {
        context = new DependencyInjectionContext();
        testQueryHandlerMock = context.RegisterQueryHandler("My expected response");

        context.BuildServiceProvider();
        queryDispatcher = context.GetService<IQueryDispatcher>();
    }

    [Fact]
    public async Task Dispatch_A_Query_Should_Return_A_Not_Null_Response()
    {
        // Arrange
        var request = new TestQuery() { Message = "My request" };

        // Act
        var response = await queryDispatcher.Dispatch<TestQuery, TestQueryResult>(request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Dispatch_A_Query_Should_Return_Expected_Response()
    {
        // Arrange
        var request = new TestQuery() { Message = "My request" };

        // Act
        var response = await queryDispatcher.Dispatch<TestQuery, TestQueryResult>(request);

        // Assert
        Assert.Equal("My expected response", response.Message);
    }

    [Fact]
    public async Task Dispatch_A_Query_Should_Call_IQueryHandler_Once()
    {
        // Arrange
        var request = new TestQuery() { Message = "My request" };

        // Act
        await queryDispatcher.Dispatch<TestQuery, TestQueryResult>(request);

        // Assert
        testQueryHandlerMock.Verify(handler => handler.Handle(It.IsAny<TestQuery>(), CancellationToken.None), Times.Exactly(1));
    }
}
