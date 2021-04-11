using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Hoff.Api.Logic.Currency.Models;

namespace Hoff.Api.Logic.Currency.Abstractions
{
    public interface ICurrencyExchangeRate
    {
        Task<ICollection<CurrencyExchangeRateModel>> GetExchangeRates(DateTime dateTime, CancellationToken cancellationToken);

        Task<CurrencyExchangeRateModel> GetExchangeRate(DateTime dateTime, string currencyCode, CancellationToken cancellationToken);
    }
}
