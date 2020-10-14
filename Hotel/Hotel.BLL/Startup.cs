using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hotel.BLL.Startup))]
namespace Hotel.BLL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
