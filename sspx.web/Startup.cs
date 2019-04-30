using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sspx.web.data;
using StructureMap;
using sspx.infra.config;
using sspx.web.Helpers;
using sspx.web.Services;

namespace sspx.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = ConfigHelper.GetSessionTimeout(Configuration["SSPX_TIMEOUT_MINUTES"]);

                // indicates this cookie is essential for the app to function correctly. NOTE: if true, then consent policy checks may be bypassed.
                options.Cookie.IsEssential = true;
            });

            services.AddCustomizedUsers(Configuration);
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = ConfigHelper.GetIdentityTimeout(Configuration["SSPX_TIMEOUT_MINUTES"]);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SSPxIdentity"))
            );

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = ConfigHelper.GetLockoutMaxFailedAttempts(Configuration["SSPX_LOGIN_LOCKOUT_MAX_ATTEMPTS"]);
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    // by default, require user to be logged in to see these pages unless overridden with [AllowAnonymous] attribute
                    options.Conventions.AuthorizeAreaFolder("Admin", "/");
                    options.Conventions.AuthorizeAreaFolder("Dashboard", "/");
                    options.Conventions.AuthorizeAreaFolder("Help", "/");
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/");
                    options.Conventions.AuthorizeAreaFolder("Protocols", "/");

                    options.Conventions.AddAreaPageRoute("Dashboard", "/Index", "");
                });

            services.AddSSPxNavMenuData(Configuration);
            services.AddSSPxProtocolData(Configuration);
            services.AddSSPxAdminSearchData(Configuration);

            services.AddScoped<IProtocolPermissions, ProtocolPermissions>();
            services.AddScoped<ICapEmails, CapEmails>();

            services.AddSingleton<ISSPxEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.Configure<PreviewOptions>(Configuration);
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup)); // sspx.web
                    // _.AssemblyContainingType(typeof(BaseEntity)); // sspx.core
                    _.Assembly("sspx.infra"); // sspx.infra
                    // TODO: NAA.
                    _.WithDefaultConventions();
                    // _.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                });

                // TODO: NAA.
                // config.For(typeof(IRepository<>)).Add(typeof(EfRepository<>));
                config.For(typeof(ISSPxConfig)).Add(typeof(SSPxConfig));

                config.For<ISSPxConfig>().Use(_ => new SSPxConfig(
                    Configuration.GetConnectionString("SSPxData")
                )).ContainerScoped();

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes => Routes.GetRoutes(routes));
        }
    }
}
