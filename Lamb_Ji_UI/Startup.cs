using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lamb_Ji_UI.Startup))]
namespace Lamb_Ji_UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
