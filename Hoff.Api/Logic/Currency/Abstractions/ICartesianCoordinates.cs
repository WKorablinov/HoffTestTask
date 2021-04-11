using Hoff.Api.Logic.Currency.Enums;

namespace Hoff.Api.Logic.Currency.Abstractions
{
    public interface ICartesianCoordinates
    {
        CartesianQuadrant GetQuadrant(int x, int y);
    }
}
