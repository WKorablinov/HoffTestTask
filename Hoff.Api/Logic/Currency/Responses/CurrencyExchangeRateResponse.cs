using System;

namespace Hoff.Api.Logic.Currency.Responses
{
    public record CurrencyExchangeRateResponse(
        DateTime DateTime,
        string Name,
        string Code,
        decimal Rate,
        decimal OneRubleRate);
}
