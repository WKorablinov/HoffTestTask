using Newtonsoft.Json;

namespace Hoff.Api.Infrastructure.Exceptions
{
    public record ErrorDetails(int StatusCode, string Message)
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
