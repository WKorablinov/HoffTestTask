using Hoff.Api.Configuration;
using Hoff.Api.Logic.Currency.Business;
using Hoff.Api.Logic.Currency.Enums;

using Microsoft.Extensions.Options;

using FluentAssertions;

using Xunit;

namespace Hoff.Api.UnitTests.Tests.Logic.Currency.Business
{
    public class CartesianCoordinateSystemTests
    {
        [Theory]
        [InlineData(0, 0, CartesianQuadrant.Axes)]
        [InlineData(0, 5, CartesianQuadrant.Axes)]
        [InlineData(0, -5, CartesianQuadrant.Axes)]
        [InlineData(5, 0, CartesianQuadrant.Axes)]
        [InlineData(-5, 0, CartesianQuadrant.Axes)]
        [InlineData(5, 5, CartesianQuadrant.I)]
        [InlineData(-5, 5, CartesianQuadrant.II)]
        [InlineData(-5, -5, CartesianQuadrant.III)]
        [InlineData(5, -5, CartesianQuadrant.IV)]
        public void GetQuadrant_PassCoordinate_ReturnQuadrant(int x, int y, CartesianQuadrant expectedQuadrant)
        {
            // Arrange
            var coordinateSystem = new CartesianCoordinateSystem();

            // Act
            var quadrant = coordinateSystem.GetQuadrant(x, y);

            // Assert
            quadrant.Should().Be(expectedQuadrant);
        }
    }
}
