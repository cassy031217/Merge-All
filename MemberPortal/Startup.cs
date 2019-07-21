using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MemberPortal.Startup))]
namespace MemberPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
