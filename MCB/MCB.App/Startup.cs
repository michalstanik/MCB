
using AutoMapper;
using MCB.App.OperationFilters;
using MCB.Data;
using MCB.Data.Repositories;
using MCB.Data.RepositoriesInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MCB.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatter = setupAction.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.mcb.trip+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.mcb.tripwithstops+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.mcb.tripwithstopsandusers+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.mcb.tripwithcountries+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.mcb.tripwithcountriesandstats+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.mcb.tripwithcountriesandworldheritages+json");

                    if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                    }
                }
            })
            .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore) //ignores self reference object 
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //validate api rules

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        var actionExecutingContext =
            //            actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

            //        // if there are modelstate errors & all keys were correctly
            //        // found/parsed we're dealing with validation errors
            //        if (actionContext.ModelState.ErrorCount > 0
            //            && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
            //        {
            //            return new UnprocessableEntityObjectResult(actionContext.ModelState);
            //        }

            //        // if one of the keys wasn't correctly found / couldn't be parsed
            //        // we're dealing with null/unparsable input
            //        return new BadRequestObjectResult(actionContext.ModelState);
            //    };
            //});

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("MCBOpenAPISpecification", 
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "MCB API",
                        Version = "1"
                    });

                setupAction.OperationFilter<TripOperationFilter>();

                //Use of reflection to cobime a XML document with assembly path
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentFullPath);

                setupAction.ResolveConflictingActions(apiDesscriptions =>
                {
                    return apiDesscriptions.First();
                });

            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/clientapp/dist";
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<MCBDictionarySeeder>();
            services.AddTransient<MCBDataSeeder>();
            services.AddScoped<IGeoRepository, GeoRepository>();
            services.AddScoped<ITripRepository, TripRepository>();

            services.AddDbContext<MCBContext>(cfg =>
            {
                if (Environment.IsDevelopment())
                {
                    cfg.UseSqlServer(Configuration.GetConnectionString("MCBConnectionString"));
                }
                else
                {
                    cfg.UseSqlServer(Configuration.GetConnectionString("MCBConnectionStringProd"));
                }
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/MCBOpenAPISpecification/swagger.json", "MCB API");
                setupAction.RoutePrefix = "api";

           
            });

            app.UseMvc();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(days: 0, hours: 0, minutes: 1, seconds: 30);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            if (env.IsDevelopment())
            {
                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var recreateDbOption = Configuration.GetSection("DevelopmentEnvironmentSettings").GetSection("RecreateDatabaseEachTime").Value;

                    //1
                    var dictionarySeeder = scope.ServiceProvider.GetService<MCBDictionarySeeder>();
                    dictionarySeeder.Seed(recreateDbOption).Wait();

                    //2
                    var dataSeeder = scope.ServiceProvider.GetService<MCBDataSeeder>();
                    dataSeeder.Seed(recreateDbOption).Wait();
                }
            }
        }
    }
}
