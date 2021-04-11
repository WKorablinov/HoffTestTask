using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Hoff.Api.Logic.Currency.Enums;
using Hoff.Api.Infrastructure.Exceptions;
using Hoff.Api.Logic.Currency.Abstractions;
using Hoff.Api.Logic.Currency.Queries;
using Hoff.Api.Logic.Currency.Responses;

using MediatR;

using Microsoft.Extensions.Options;

using Hoff.Api.Configuration;

namespace Hoff.Api.Logic.Currency.Handlers
{
    public class GetCurrencyExchangeRateHandler : IRequestHandler<GetCurrencyExchangeRateQuery, CurrencyExchangeRateResponse>
    {
        private readonly IGeometry _geometry;
        private readonly ICurrencyExchangeRate _currencyRate;
        private readonly ICartesianCoordinates _coordinates;
        private readonly IOptions<ApiOptions> _apiOptions;

        public GetCurrencyExchangeRateHandler(
            IGeometry geometry,
            ICurrencyExchangeRate currencyRate,
            ICartesianCoordinates coordinates,
            IOptions<ApiOptions> apiOptions)
        {
            _geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            _currencyRate = currencyRate ?? throw new ArgumentNullException(nameof(currencyRate));
            _coordinates = coordinates ?? throw new ArgumentNullException(nameof(geometry));
            _apiOptions = apiOptions ?? throw new ArgumentNullException(nameof(apiOptions));
        }

        public async Task<CurrencyExchangeRateResponse> Handle(GetCurrencyExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var hit = _geometry.Hit(request.X, request.Y);

            if (hit == false)
                throw new BusinessLogicException("Не попал в круг.");

            var quadrant = _coordinates.GetQuadrant(request.X, request.Y);

            if (quadrant == CartesianQuadrant.Axes)
                throw new BusinessLogicException("Попадание в оси коодринат. Квадрант не определён.");

            var dates = GetDates();

            var date = dates[quadrant];

            var exchangeRate = await _currencyRate.GetExchangeRate(date, _apiOptions.Value.CurrencyCode, cancellationToken);

            if (exchangeRate.Code == "-1")
                throw new BusinessLogicException($"Не удалось определить курс для валюты с кодом {_apiOptions.Value.CurrencyCode}.");

            return new CurrencyExchangeRateResponse(
                date,
                exchangeRate.Name,
                exchangeRate.Code,
                exchangeRate.Rate,
                exchangeRate.Nom / exchangeRate.Rate);
        }

        private IDictionary<CartesianQuadrant, DateTime> GetDates()
        {
            var dates = new Dictionary<CartesianQuadrant, DateTime>();

            dates.Add(CartesianQuadrant.I, DateTime.Now);
            dates.Add(CartesianQuadrant.II, DateTime.Now.AddDays(-1));
            dates.Add(CartesianQuadrant.III, DateTime.Now.AddDays(-2));
            dates.Add(CartesianQuadrant.IV, DateTime.Now.AddDays(1));

            return dates;
        }
    }
}
