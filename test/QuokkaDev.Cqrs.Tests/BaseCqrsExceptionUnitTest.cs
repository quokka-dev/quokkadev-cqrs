using FluentAssertions;
using QuokkaDev.Cqrs.Abstractions.Exceptions;
using System;
using Xunit;

namespace QuokkaDev.Cqrs.Tests
{
    public class BaseCqrsExceptionUnitTest
    {
        public BaseCqrsExceptionUnitTest()
        {
        }

        [Fact]
        public void Errors_Passed_To_Constructor_Should_Be_Used()
        {
            // Arrange
            var baseEx = new BaseCqrsException(new string[] { "MyError" });

            // Act            

            // Assert
            baseEx.Errors.Should().NotBeEmpty().And.Contain("MyError");
        }

        [Fact]
        public void Error_Passed_To_Constructor_Should_Be_Used()
        {
            // Arrange
            var innerEx = new Exception("inner");
            var baseEx = new BaseCqrsException("MyError", innerEx);

            // Act            

            // Assert
            baseEx.Errors.Should().NotBeEmpty().And.Contain("MyError");
            baseEx.InnerException.Should().NotBeNull();
        }
    }
}
