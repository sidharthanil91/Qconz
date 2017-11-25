using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QconzLocate.Startup))]
namespace QconzLocate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
