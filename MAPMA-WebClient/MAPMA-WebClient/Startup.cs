using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MAPMA_WebClient.Startup))]
namespace MAPMA_WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
