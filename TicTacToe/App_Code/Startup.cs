using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicTacToe.Startup))]
namespace TicTacToe
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
