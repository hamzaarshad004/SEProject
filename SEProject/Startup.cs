using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SEProject.Startup))]
namespace SEProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
