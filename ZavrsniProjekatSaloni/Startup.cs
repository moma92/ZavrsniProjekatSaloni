using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZavrsniProjekatSaloni.Startup))]
namespace ZavrsniProjekatSaloni
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
