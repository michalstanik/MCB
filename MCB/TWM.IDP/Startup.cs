using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using TWM.IDP.Data;

namespace TWM.IDP
{
    public class Startup
    {
        private readonly string _connectionString;
        private readonly string _migrationsAssembly;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            _connectionString = Configuration.GetConnectionString("Default");
            _migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        }
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseSqlServer(_connectionString, sqlOptions => sqlOptions.MigrationsAssembly(_migrationsAssembly)));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc();
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddOperationalStore(options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(_connectionString, sqlOptions => sqlOptions.MigrationsAssembly(_migrationsAssembly)))
                .AddConfigurationStore(options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(_connectionString, sqlOptions => sqlOptions.MigrationsAssembly(_migrationsAssembly)))
                .AddAspNetIdentity<IdentityUser>();


            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //Only in Dev and should be manage by parameter
            InitializeDbTestData(app);

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
        private static void InitializeDbTestData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {

                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.EnsureDeleted();

                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in Clients.Get())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Resources.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Resources.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (!userManager.Users.Any())
                {
                    foreach (var testUser in Users.Get())
                    {
                        var identityUser = new IdentityUser(testUser.Username)
                        {
                            Id = testUser.SubjectId
                        };

                        userManager.CreateAsync(identityUser, "Password123!").Wait();
                        userManager.AddClaimsAsync(identityUser, testUser.Claims.ToList()).Wait();
                    }
                }
            }
        }

    }
}
