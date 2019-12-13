using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OSRSBossTracker.Startup))]
namespace OSRSBossTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
