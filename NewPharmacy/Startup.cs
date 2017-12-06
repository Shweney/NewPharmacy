using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewPharmacy.Startup))]
namespace NewPharmacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
