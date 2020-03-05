using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stock_System2.Startup))]
namespace Stock_System2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
