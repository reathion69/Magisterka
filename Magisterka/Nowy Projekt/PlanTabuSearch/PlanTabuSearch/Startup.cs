using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlanTabuSearch.Startup))]
namespace PlanTabuSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
