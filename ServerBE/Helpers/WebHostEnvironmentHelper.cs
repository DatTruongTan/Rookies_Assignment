using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Helpers
{
    public class WebHostEnvironmentHelper
    {
        public static string GetWebRootPath()
        {
            var _ancessor = new HttpContextAccessor();

            return _ancessor.HttpContext
                            .RequestServices
                            .GetRequiredService<IWebHostEnvironment>()
                            .WebRootPath;
        }

        public static string GetWebUrl()
        {
            var _ancessor = new HttpContextAccessor();

            return _ancessor.HttpContext
                            .RequestServices
                            .GetRequiredService<IConfiguration>()
                            .GetValue<string>(ConstConfiguration.BACK_END_ENDPOINT);
        }
    }
}
