using CLOUD.BLL.Enterfaces;
using CLOUD.BLL.Services.TokenService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLOUD.BLL.DI
{
    public static class Bootstrap
    {
        public static void DI(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

        }
    }
}
