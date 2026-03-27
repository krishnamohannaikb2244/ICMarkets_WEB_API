using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ICMarkets.API.HealthChecks
{
    public class BlockcypherHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _url;
        private readonly string _componentName;

        public BlockcypherHealthCheck(IHttpClientFactory httpClientFactory, string url, string componentName)
        {
            _httpClientFactory = httpClientFactory;
            _url = url;
            _componentName = componentName;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var response = await client.GetAsync(_url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy($"{_componentName} is reachable");
                }

                return HealthCheckResult.Unhealthy($"{_componentName} returned {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"{_componentName} is unreachable", ex);
            }
        }
    }
}
