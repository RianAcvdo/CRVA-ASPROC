using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agape.ReservacionVhl.UIEmpleadoMVC.Startup))]
namespace Agape.ReservacionVhl.UIEmpleadoMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
