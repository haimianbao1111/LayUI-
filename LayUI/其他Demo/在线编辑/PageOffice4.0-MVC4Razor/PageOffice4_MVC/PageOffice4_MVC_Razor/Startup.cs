using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PageOffice4_MVC_Razor.Startup))]
namespace PageOffice4_MVC_Razor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
