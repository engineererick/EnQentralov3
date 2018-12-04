using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnQentralo.Backend.Startup))]
namespace EnQentralo.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
