using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BackendCook.Startup))]
namespace BackendCook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
