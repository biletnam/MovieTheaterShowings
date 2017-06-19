using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.FrontEnd.Startup))]
namespace MVC.FrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
