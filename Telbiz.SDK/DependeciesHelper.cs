using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.SDK.Configurations;
using Telbiz.SDK.Sms;
using Telbiz.SDK.Token;
using Telbiz.SDK.Topup;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependeciesHelper
    {
        public static void RegisterTelbizDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Configuration
            var envConfig = configuration.GetSection(nameof(TelbizCredential)).Get<TelbizCredential>();
            services.AddSingleton(envConfig);
            
            // Register Token Service
            services.AddSingleton<ITokenInterface, TokenInterface>();

            // Register SMS Service
            services.AddSingleton<ISMSInterface, SMSInterface>();

            // Register Topup Service
            services.AddSingleton<ITopupInterface, TopupInterface>();
        }
    }
}
