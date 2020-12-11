using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRentalsApplication.Startup))]
namespace CarRentalsApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
