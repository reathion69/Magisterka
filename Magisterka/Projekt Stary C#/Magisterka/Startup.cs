using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Magisterka.Startup))]
namespace Magisterka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
