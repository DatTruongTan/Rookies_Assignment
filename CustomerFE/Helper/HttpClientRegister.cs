using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CustomerFE.Helper
{
    public static class HttpClientRegister
    {
        public static void AddCustomHttpClient(this IServiceCollection services, IConfiguration config)
        {
            var configureClient = new Action<IServiceProvider, HttpClient>(async (provider, client) =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                var accessToken = await httpContextAccessor
                                            .HttpContext
                                            .GetTokenAsync(ConstRequest.ACCESS_TOKEN);

                client.BaseAddress = new Uri(config[ConstConfiguration.BACK_END_ENDPOINT]);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(ConstRequest.BEARER, accessToken);
            });

            services.AddHttpClient(ConstService.BACK_END_NAMED_CLIENT, configureClient);
        }
    }
}
