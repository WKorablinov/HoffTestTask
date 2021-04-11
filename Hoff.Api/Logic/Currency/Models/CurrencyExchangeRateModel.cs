namespace Hoff.Api.Logic.Currency.Models
{
    public record CurrencyExchangeRateModel(
        string Name,
        string Code,
        decimal Rate,
        int Nom
        );    
}
