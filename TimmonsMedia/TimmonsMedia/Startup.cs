using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimmonsMedia.Startup))]
namespace TimmonsMedia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
