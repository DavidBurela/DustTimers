using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DustTimers.Web.Startup))]
namespace DustTimers.Web
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
