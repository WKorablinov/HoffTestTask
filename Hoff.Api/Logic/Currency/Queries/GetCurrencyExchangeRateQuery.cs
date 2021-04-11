using Hoff.Api.Logic.Currency.Responses;

using MediatR;

namespace Hoff.Api.Logic.Currency.Queries
{
    public record GetCurrencyExchangeRateQuery(int X, int Y) : IRequest<CurrencyExchangeRateResponse>;
}
