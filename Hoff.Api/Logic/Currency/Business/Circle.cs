using System;

using Hoff.Api.Configuration;
using Hoff.Api.Logic.Currency.Abstractions;

using Microsoft.Extensions.Options;

namespace Hoff.Api.Logic.Currency.Business
{
    public class Circle : IGeometry
    {
        private readonly int _x0 = 0;
        private readonly int _y0 = 0;

        private readonly IOptions<GeometryOptions> _geometryOptions;

        public Circle(IOptions<GeometryOptions> geometryOptions)
        {
            _geometryOptions = geometryOptions ?? throw new ArgumentNullException(nameof(geometryOptions));
        }

        public bool Hit(int x, int y) => (Math.Pow(x - _x0, 2) + Math.Pow(y - _y0, 2)) <= Math.Pow(_geometryOptions.Value.Radius, 2);
    }
}
