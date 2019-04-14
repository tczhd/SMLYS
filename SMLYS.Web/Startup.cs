using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMLYS.Infrastructure.Data;
using SMLYS.Infrastructure.Identity;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.Infrastructure.Data.Repository.Base;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using SMLYS.ApplicationCore.Services.Patients;
using SMLYS.Infrastructure.Services.Email;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.Services.Api;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.Interfaces.Services.Users;
using SMLYS.ApplicationCore.Services.Users;
using SMLYS.ApplicationCore.Services.Items;
using SMLYS.ApplicationCore.Interfaces.Services.Items;
using SMLYS.ApplicationCore.Interfaces.Services.Utiliites;
using SMLYS.ApplicationCore.Services.Utiliites;
using SMLYS.ApplicationCore.Interfaces.Base;
using SMLYS.Infrastructure.Configuration.Sms;
using SMLYS.Infrastructure.Services.SMS;
using SMLYS.ApplicationCore.Services.Doctors;
using SMLYS.ApplicationCore.Interfaces.Services.Doctor;

namespace SMLYS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SMLYSContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection")
               , b => b.MigrationsAssembly("SMLYS.Infrastructure")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                    , b => b.MigrationsAssembly("SMLYS.Infrastructure")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<SMSoptions>(Configuration.GetSection("TwilioAccountDetails"));

            ConfigureApplicatiojnService(services);
            ConfigureWebService(services);
            // Add application services.

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddSessionStateTempDataProvider();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<UserHandler>();

            services.AddTransient<IEmailSender, TwilioAuthMessageSender>();
            services.AddTransient<ISmsSender, TwilioAuthMessageSender>();
            services.Configure<SMSoptions>(Configuration);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            // IMPORTANT: This session call MUST go before UseMvc()
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureWebService(IServiceCollection services)
        {
            services.AddScoped<IPatientApiService, PatientApiService>();
        }

        private void ConfigureApplicatiojnService(IServiceCollection services)
        {
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IDoctorService, DoctorService>();
        }
    }
}
