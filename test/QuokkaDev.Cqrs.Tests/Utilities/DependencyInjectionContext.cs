using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using QuokkaDev.Cqrs.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace QuokkaDev.Cqrs.Tests.Utilities
{
    /// <summary>
    /// Help class for setup dependency injection and track mock objects
    /// </summary>
    internal class DependencyInjectionContext
    {
        private readonly IServiceCollection services;
        private readonly IList<Mock> mocks;
        private ServiceProvider? serviceProvider;

        public DependencyInjectionContext()
        {
            services = new ServiceCollection();
            services.AddCQRS();
            mocks = new List<Mock>();
        }

        public DependencyInjectionContext(Assembly assembly)
        {
            services = new ServiceCollection();
            services.AddCQRS(assembly);
            mocks = new List<Mock>();
        }

        public DependencyInjectionContext(Assembly[] assemblies)
        {
            services = new ServiceCollection();
            services.AddCQRS(assemblies);
            mocks = new List<Mock>();
        }

        /// <summary>
        /// Register a Mock ILogger<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the logger</typeparam>
        /// <param name="services">The service collection where register the log</param>
        /// <returns>The original service collection. Used for chaining multiple calls</returns>
        public Mock<ILogger<T>> RegisterMockLogger<T>()
        {
            var loggerMock = new Mock<ILogger<T>>();
            mocks.Add(loggerMock);
            services.AddSingleton(loggerMock.Object);
            return loggerMock;
        }

        /// <summary>
        /// Build the service provider after the dependency injection configuration
        /// </summary>
        public void BuildServiceProvider()
        {
            serviceProvider = services.BuildServiceProvider();
            services.AddSingleton(serviceProvider);
        }

        /// <summary>
        /// Retrieve a service from the dependency injection container
        /// </summary>
        /// <typeparam name="T">Type of the requested service</typeparam>
        /// <returns>The service</returns>
        /// <exception cref="InvalidOperationException">Raised if the service provider is not initialized</exception>
        public T GetService<T>() where T : class
        {
            if(serviceProvider != null)
            {
                return serviceProvider.GetRequiredService<T>();
            }
            else
            {
                throw new InvalidOperationException("Service provider is not initialized. Ensure to call BuildServiceProvider()");
            }
        }

        /// <summary>
        /// Register a query handler for test purpose
        /// </summary>
        /// <param name="expectedResponse">The response expected from the handler</param>
        /// <returns>The mocked query handler</returns>
        public Mock<IQueryHandler<TestQuery, TestQueryResult>> RegisterQueryHandler(string expectedResponse = TestQueryResult.DEFAULT_RESPONSE)
        {
            var testQueryHandlerMock = new Mock<IQueryHandler<TestQuery, TestQueryResult>>();
            testQueryHandlerMock
                .Setup(handler => handler.Handle(It.IsAny<TestQuery>(), CancellationToken.None))
                .ReturnsAsync(new TestQueryResult() { Message = expectedResponse });

            mocks.Add(testQueryHandlerMock);

            services.AddSingleton(testQueryHandlerMock.Object);

            return testQueryHandlerMock;
        }

        /// <summary>
        /// Register a command handler for test purpose
        /// </summary>
        /// <param name="expectedResponse">The response expected from the handler</param>
        /// <returns>The mocked command handler</returns>
        public Mock<ICommandHandler<TestCommand, TestCommandResult>> RegisterCommandHandler(string expectedResponse = TestCommandResult.DEFAULT_RESPONSE)
        {
            var testCommandHandlerMock = new Mock<ICommandHandler<TestCommand, TestCommandResult>>();
            testCommandHandlerMock
                .Setup(handler => handler.Handle(It.IsAny<TestCommand>(), CancellationToken.None))
                .ReturnsAsync(new TestCommandResult() { Message = expectedResponse });

            mocks.Add(testCommandHandlerMock);

            services.AddSingleton(testCommandHandlerMock.Object);

            return testCommandHandlerMock;
        }
    }
}
