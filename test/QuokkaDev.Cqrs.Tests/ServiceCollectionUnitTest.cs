using FluentAssertions;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Cqrs.Tests.Utilities;
using System;
using System.Reflection;
using System.Threading;
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
        public void Registration_With_Assembly_Should_Register_Handlers_In_Assembly()
        {
            // Arrange 
            DependencyInjectionContext context = new DependencyInjectionContext(typeof(ServiceCollectionUnitTest).Assembly);
            TestRegistration(context);
        }

        [Fact]
        public void Registration_With_Empty_Array_Should_Register_Handlers_In_Current_Assembly()
        {
            // Arrange 
            DependencyInjectionContext context = new DependencyInjectionContext(Array.Empty<Assembly>());
            TestRegistration(context);
        }

        private static void TestRegistration(DependencyInjectionContext context)
        {
            // Arrange  
            context.BuildServiceProvider();

            // Act
            var commadHandler = context.GetService<ICommandHandler<string, string>>();
            var queryHandler = context.GetService<IQueryHandler<string, string>>();

            // Assert
            commadHandler.Should().NotBeNull("command handler in test assembly must be used");
            queryHandler.Should().NotBeNull("query handler in test assembly must be used");

            commadHandler.Should().BeOfType<MyFakeCommandHandler>("MyFakeCommandHandler must be resolved");
            queryHandler.Should().BeOfType<MyFakeQueryHandler>("MyFakeQueryHandler must be resolved");
        }
    }

    public class MyFakeQueryHandler : IQueryHandler<string, string>
    {
        public Task<string> Handle(string query, CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MyFakeCommandHandler : ICommandHandler<string, string>
    {
        public Task<string> Handle(string query, CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }
    }
}
