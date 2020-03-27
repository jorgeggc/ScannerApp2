using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScannerApp2.Startup))]
namespace ScannerApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}