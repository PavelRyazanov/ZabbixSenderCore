using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZabbixSenderCore
{
    public interface IZabbixSenderService
    {
        Task<ZabbixResponse> Send(string host, string key, string value, DateTime? dateTime = null, CancellationToken cancellationToken = default);

        Task<ZabbixResponse> Send(string host, string key, int value, DateTime? dateTime = null, CancellationToken cancellationToken = default);

        Task<ZabbixResponse> Send(string host, string key, float value, DateTime? dateTime = null, CancellationToken cancellationToken = default);

        Task<ZabbixResponse> Send(ZabbixData data, CancellationToken cancellationToken = default);
    }
}