using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;


namespace TCub
{
    public class Startup
    {
        //public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            //var configuration = new ConfigurationBuilder(env.WebRootPath)
            //    .AddJsonFile("config.json")
            //    .AddEnvironmentVariables();

            //Configuration = configuration.Build();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

           // services.AddSingleton(_ => Configuration);
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //http://www.mikesdotnetting.com/article/269/ac
            //app.UseInstagramMiddleware();


            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
        }
    }
}
