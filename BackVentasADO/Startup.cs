using Microsoft.Owin;
using Microsoft.Owin.Cors;  // <-- agregar
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;

[assembly: OwinStartup(typeof(BackVentasADO.Startup))]

namespace BackVentasADO
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configuración de CORS para SignalR
            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context =>
                    {
                        var policy = new CorsPolicy
                        {
                            AllowAnyMethod = true,
                            AllowAnyHeader = true,
                            SupportsCredentials = true   // importante
                        };
                        // Reemplaza con la URL de tu Angular (puede ser localhost:4200)
                        policy.Origins.Add("http://localhost:4200");
                        return Task.FromResult(policy);
                    }
                }
            });

            // Mapear SignalR (después de CORS)
            app.MapSignalR();
        }
    }
}
