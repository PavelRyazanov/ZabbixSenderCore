using ZabbixSenderCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ZabbixSenderServiceCollectionExtensions
    {
        public static IServiceCollection AddZabbixService(this IServiceCollection services)
        {
            services.AddTransient<IZabbixSenderService, ZabbixSender>();
            return services;
        }
    }
}