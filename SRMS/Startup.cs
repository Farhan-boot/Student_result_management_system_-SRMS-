using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SRMS.Startup))]
namespace SRMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
