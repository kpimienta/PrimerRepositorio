using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EjemploNetAngular.Models;
//using NJsonSchema;
//using NSwag.AspNetCore;

namespace EjemploNetAngular {
    public class Startup {  
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<TaskContext> (opt => opt.UseSqlServer (@"Server=(LocalDB)\MSSQLLocalDB;Database=TaskBD;Trusted_Connection=True;"));
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            // In production, the Angular files will be served from this directory
            /* services.AddSwaggerDocument(config=>{
                config.PostProcess = document =>
                {
                    document.Info.Version ="v1";
                    document.Info.Title ="Task API";
                    document.Info.Description= "A simple ASP.NET Core web API";
                    document.Info.TermsOfService ="None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name= "Prog Web",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name ="Use under LICX",
                        Url= "https://example.com/license"
                    };
                };

             });*/
            services.AddSpaStaticFiles (configuration => {
                configuration.RootPath = "ClientApp/dist";
            });
        } // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            //app.UseSwagger();
            //app.UseSwaggerUi3();
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseSpaStaticFiles ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa (spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment ()) {
                    spa.UseAngularCliServer (npmScript: "start");
                }
            });
        }
    }
}