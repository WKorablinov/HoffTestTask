using Hoff.Api.Configuration;
using Hoff.Api.Logic.Currency.Business;

using Microsoft.Extensions.Options;

using FluentAssertions;

using Xunit;

namespace Hoff.Api.UnitTests.Tests.Logic.Currency.Business
{
    public class CircleTests
    {
        [Theory]
        [InlineData(0, 0, 5)]
        [InlineData(5, 0, 5)]
        [InlineData(-5, 0, 5)]
        [InlineData(0, -5, 5)]
        [InlineData(0, 5, 5)]
        [InlineData(2, 2, 5)]
        [InlineData(-2, -2, 5)]
        [InlineData(2, -2, 5)]
        [InlineData(-2, 2, 5)]
        public void Hit_PassCoordinateInCircle_ReturnTrue(int x, int y, int radius)
        {
            // Arrange
            var geometryOptions = Options.Create(new GeometryOptions { Radius = radius });
            var circle = new Circle(geometryOptions);

            // Act
            var hit = circle.Hit(x, y);

            // Assert
            hit.Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 5, 5)]
        [InlineData(-5, -5, 5)]
        [InlineData(5, -5, 5)]
        [InlineData(-5, 5, 5)]
        public void Hit_PassCoordinateOutsideCircle_ReturnTrue(int x, int y, int radius)
        {
            // Arrange
            var geometryOptions = Options.Create(new GeometryOptions { Radius = radius });
            var circle = new Circle(geometryOptions);

            // Act
            var hit = circle.Hit(x, y);

            // Assert
            hit.Should().BeFalse();
        }
    }
}
