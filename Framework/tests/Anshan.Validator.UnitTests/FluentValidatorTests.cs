using System;
using System.Threading;
using Anshan.Validator.FluentValidation;
using Anshan.Validator.UnitTests.Data;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Anshan.Validator.UnitTests
{
    public class FluentValidatorTests
    {
        [Fact]
        public void OnValidateAsync_Should_CallServiceProvider()
        {
            // Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            var fluentValidator = new FluentValidator(serviceProvider);

            // Act
            fluentValidator.ValidateAsync(new FakeValidatableClass());

            // Assert
            serviceProvider.Received(1).GetService(typeof(IValidator<FakeValidatableClass>));
        }

        [Fact]
        public void OnValidateAsync_Should_CallServiceProvider_When_UsingNonGenericValidateMethod()
        {
            // Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            var fluentValidator = new FluentValidator(serviceProvider);
            var value = new FakeValidatableClass();

            // Act
            fluentValidator.ValidateAsync((object) value);

            // Assert
            serviceProvider.Received(1).GetService(typeof(IValidator<FakeValidatableClass>));
        }

        [Fact]
        public void OnValidateAsync_Should_CallValidator()
        {
            // Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validator = Substitute.For<IValidator<FakeValidatableClass>>();

            serviceProvider
                .GetService(Arg.Any<Type>())
                .Returns(validator);

            var fluentValidator = new FluentValidator(serviceProvider);

            // Act
            fluentValidator.ValidateAsync(new FakeValidatableClass());

            // Assert
            validator
                .Received(1)
                .ValidateAsync(Arg.Any<FakeValidatableClass>(), Arg.Any<CancellationToken>());
        }
    }
}