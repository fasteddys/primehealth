using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DispatchingMVCVer1.Startup))]
namespace DispatchingMVCVer1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
