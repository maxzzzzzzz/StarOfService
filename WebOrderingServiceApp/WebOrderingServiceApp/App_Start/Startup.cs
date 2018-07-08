

using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WebOrderingServiceApp.Models;

[assembly: OwinStartup(typeof(WebOrderingServiceApp.App_Start.Startup))]

namespace WebOrderingServiceApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
            //Database.SetInitializer<ApplicationContext>(new LaunchRole());
            //app.CreatePerOwinContext<ApplicationContext>(new LaunchRole());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            //builder.RegisterModule(new AutofacWebTypesModule());
           // DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);//Set the WebApi DependencyResolver

            // REGISTER WITH OWIN
            //app.UseAutofacMiddleware(container);
            //app.UseAutofacMvc();

           // ConfigureAuth(app);
        }

    }
}
