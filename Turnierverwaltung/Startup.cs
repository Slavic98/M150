using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Turnierverwaltung.Startup))]
namespace Turnierverwaltung
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
