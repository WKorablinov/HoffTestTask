using System.Collections.Generic;
using System.Linq;

using Hoff.Api.Logic.Currency.Abstractions;
using Hoff.Api.Logic.Currency.Enums;

namespace Hoff.Api.Logic.Currency.Business
{
    public class CartesianCoordinateSystem : ICartesianCoordinates
    {
        private readonly List<IQuadrant> _quadrants = new List<IQuadrant>
        {
            new Axes(),
            new QuadrantI(),
            new QuadrantII(),
            new QuadrantIII(),
            new QuadrantIV()
        };

        public CartesianCoordinateSystem()
        { }

        public CartesianQuadrant GetQuadrant(int x, int y)
        {
            var quadrant = _quadrants.Single(q => q.Hit(x, y));
            return quadrant.Type;
        }

        private class Axes : IQuadrant
        {
            public CartesianQuadrant Type { get; } = CartesianQuadrant.Axes;

            public bool Hit(int x, int y) => x == 0 || y == 0;
        }

        private class QuadrantI : IQuadrant
        {
            public CartesianQuadrant Type { get; } = CartesianQuadrant.I;

            public bool Hit(int x, int y) => x > 0 && y > 0;
        }

        private class QuadrantII : IQuadrant
        {
            public CartesianQuadrant Type { get; } = CartesianQuadrant.II;

            public bool Hit(int x, int y) => x < 0 && y > 0;
        }

        private class QuadrantIII : IQuadrant
        {
            public CartesianQuadrant Type { get; } = CartesianQuadrant.III;

            public bool Hit(int x, int y) => x < 0 && y < 0;
        }

        private class QuadrantIV : IQuadrant
        {
            public CartesianQuadrant Type { get; } = CartesianQuadrant.IV;

            public bool Hit(int x, int y) => x > 0 && y < 0;
        }
    }
}
