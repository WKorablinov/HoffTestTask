using System;
using System.Threading.Tasks;

using Hoff.Api.Logic.Currency.Queries;
using Hoff.Api.Logic.Currency.Responses;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Hoff.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet("exchangeRate/{x}/{y}")]
        public async Task<CurrencyExchangeRateResponse> GetCurrencyExchangeRate(int x, int y)
        {
            var query = new GetCurrencyExchangeRateQuery(x, y);
            var result = await _mediator.Send(query);
            return result;            
        }
    }
}
