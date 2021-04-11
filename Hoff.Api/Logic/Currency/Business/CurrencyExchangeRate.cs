using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;

using DailyInfo;

using Hoff.Api.Configuration;
using Hoff.Api.Logic.Currency.Abstractions;
using Hoff.Api.Logic.Currency.Models;

using Microsoft.Extensions.Options;

namespace Hoff.Api.Logic.Currency.Business
{
    public class CurrencyExchangeRate : ICurrencyExchangeRate
    {
        private readonly IOptions<DailyInfoWebServiceOptions> _serviceOptions;

        public CurrencyExchangeRate(IOptions<DailyInfoWebServiceOptions> serviceOptions)
        {
            _serviceOptions = serviceOptions ?? throw new ArgumentNullException(nameof(serviceOptions));
        }

        public async Task<ICollection<CurrencyExchangeRateModel>> GetExchangeRates(DateTime dateTime, CancellationToken cancellationToken)
        {
            using (var client = CreateDailyInfoSoapClient())
            {
                cancellationToken.Register(() => client.Abort());

                var cursResponse = await client.GetCursOnDateAsync(dateTime);

                var result = ToModel(cursResponse).ToArray();
                return result;
            }
        }

        public async Task<CurrencyExchangeRateModel> GetExchangeRate(DateTime dateTime, string currencyCode, CancellationToken cancellationToken)
        {
            var rates = await GetExchangeRates(dateTime, cancellationToken);
            var result = rates.SingleOrDefault(x => x.Code == currencyCode) ?? new CurrencyExchangeRateModel(string.Empty, "-1", 0, 0);
            return result;
        }

        private IEnumerable<CurrencyExchangeRateModel> ToModel(ArrayOfXElement arrayOfXElement)
        {
            var xmlElement = arrayOfXElement.Nodes[1];

            var valutes = xmlElement.XPathSelectElements("//ValuteData");

            foreach (var valute in valutes)
            {
                foreach (var valuteCursOnDate in valute.Elements("ValuteCursOnDate"))
                {
                    yield return new CurrencyExchangeRateModel(
                        valuteCursOnDate.Element("Vname").Value.Trim(),
                        valuteCursOnDate.Element("VchCode").Value.Trim(),
                        decimal.Parse(valuteCursOnDate.Element("Vcurs").Value.Trim(), CultureInfo.InvariantCulture),
                        int.Parse(valuteCursOnDate.Element("Vnom").Value.Trim(), CultureInfo.InvariantCulture)
                        );
                }
            }
        }

        private DailyInfoSoapClient CreateDailyInfoSoapClient()
        {
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = 104857600 };
            var endpoint = new EndpointAddress(new Uri(_serviceOptions.Value.Url));
            var cartServiceClient = new DailyInfoSoapClient(binding, endpoint);
            return cartServiceClient;
        }
    }
}
