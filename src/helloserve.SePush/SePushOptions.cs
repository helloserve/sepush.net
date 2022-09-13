using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace helloserve.SePush
{
    public class SePushOptionsProvider : IConfigureOptions<SePushOptions>
    {
        readonly IConfiguration config;

        public SePushOptionsProvider(IConfiguration config)
        {
            this.config = config;
        }

        public void Configure(SePushOptions options)
        {
            config.GetSection(nameof(SePushOptions)).Bind(options);
        }
    }

    public class SePushOptions
    {
        public string Token { get; set; }

        public string ApiUrl { get; set; } = "https://developer.sepush.co.za/business/";

        public string Version { get; set; } = "2.0";
    }
}
