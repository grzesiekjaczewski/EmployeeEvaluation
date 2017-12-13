using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeEvaluation.Startup))]
namespace EmployeeEvaluation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
