
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using System.Reflection;
using System.Net.Http.Formatting;
using System.Web.Http.Dependencies;

namespace OwinApp
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new AutofacModule());
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
 
            // app.UseAutofacMiddleware(container);

            // app.UseAutofacWebApi(config);
            app.UseWebApi(config);


        }
    }
}
