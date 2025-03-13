using Microsoft.Extensions.Options;
using Consul;
using AccountService.Model;
using Microsoft.Extensions.Configuration;

namespace AccountService.Services
{
    public class ConsulHostedService : IHostedService
    {
        private readonly IConsulClient _consulClient;
        private readonly ILogger<ConsulHostedService> _logger;
        private string _registrationId;
        private IConfiguration _configuration;

        public ConsulHostedService(IConsulClient consulClient, ILogger<ConsulHostedService> logger, IConfiguration configuration)
        {
            _consulClient = consulClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var registration = new AgentServiceRegistration
            {
                ID = _registrationId = Guid.NewGuid().ToString(),
                Name = _configuration["ConsulConfig:ServiceName"],
                Address = _configuration["ConsulConfig:ServiceAddress"],
                Tags = ["api"],
                Port = Convert.ToInt16(_configuration["ConsulConfig:ServicePort"])
            };

            _logger.LogInformation("Registering with Consul");
            await _consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deregistering from Consul");
            await _consulClient.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }

    }
}