using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HMS.WEB.Startup))]
namespace HMS.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
