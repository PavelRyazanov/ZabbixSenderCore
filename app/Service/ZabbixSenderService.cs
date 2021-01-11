using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ZabbixSenderCore
{
    public class ZabbixSenderService : IZabbixSenderService
    {
        private readonly ILogger<ZabbixSenderService> _logger;
        private readonly IConfigurationSection _configuration;
        public ZabbixSenderService(ILogger<ZabbixSenderService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration.GetSection("Zabbix");
        }

        public async Task<bool> Send(string host, string key, string value, CancellationToken stoppingToken)
        {
            var serverAddress = _configuration.GetValue<string>("ServerAddress");
            var serverPort = _configuration.GetValue<int>("ServerPort");
            var connectionTimeout = _configuration.GetValue<int>("ConnectionTimeout");
            var sender = new ZabbixSenderCore.ZabbixSender(serverAddress, serverPort, connectionTimeout);

            var result = false;
            try
            {
                var response = await sender.Send(host, key, value, null, stoppingToken);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed send zabbix status");
            }

            return result;
        }
    }
}