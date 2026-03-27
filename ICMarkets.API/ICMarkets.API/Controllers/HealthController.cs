using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace ICMarkets.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();
            
            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(x => new
                {
                    component = x.Key,
                    status = x.Value.Status.ToString(),
                    description = x.Value.Description,
                    exception = x.Value.Exception?.Message,
                    duration = x.Value.Duration.ToString()
                }),
                totalDuration = report.TotalDuration.ToString()
            };

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            
            return report.Status == HealthStatus.Healthy 
                ? Ok(response) 
                : StatusCode(503, response);
        }
    }
}
