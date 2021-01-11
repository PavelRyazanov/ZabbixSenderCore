using System.Threading;
using System.Threading.Tasks;

namespace ZabbixSenderCore
{
    public interface IZabbixSenderService
    {
        Task<bool> Send(string host, string key, string value, CancellationToken stoppingToken);
    }
}