using helloserve.SePush;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSePush(this IServiceCollection services)
        {
            services.ConfigureOptions<SePushOptionsProvider>();
            services.AddSingleton<ISePush, SePushClient>();
            return services;
        }

        public static IServiceCollection AddSePush(this IServiceCollection services, Action<SePushOptions> config)
        {
            SePushOptions options = new SePushOptions();
            config(options);
            services.ConfigureOptions(options);
            services.AddSingleton<ISePush, SePushClient>();
            return services;
        }
    }
}
