using Hoff.Api.Logic.Currency.Enums;

namespace Hoff.Api.Logic.Currency.Abstractions
{
    public interface IQuadrant
    {
        CartesianQuadrant Type { get; }

        bool Hit(int x, int y);
    }
}
