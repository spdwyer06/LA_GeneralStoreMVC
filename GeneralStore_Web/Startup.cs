using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeneralStore_Web.Startup))]
namespace GeneralStore_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
