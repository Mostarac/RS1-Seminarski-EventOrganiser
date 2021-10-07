using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClientNotifications.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.AspNetCore;
using webapp.Areas.Organizer.Helpers;
using webapp.Areas.Organizer.Repositories;
using webapp.Context;
using webapp.Helpers;
using webapp.Infrastructure;
using webapp.Models;
using webapp.Services.Implementation;
using webapp.Services.Interfaces;

namespace webapp
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
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/Login";
                options.LoginPath = "/Account/Login";
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<AppUser, IdentityRole>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            string validIssuer = "https://p1801.app.fit.ba";
            string validAudience = "https://angular.p1801.app.fit.ba";

           services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@3456"))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IUserNotificationService, UserNotificationService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();     
            services.AddTransient<IEventRatingService, EventRatingService>();
            services.AddToastNotification();
            services.AddSignalR();

            services.AddScoped<IExceptionRepository, ExceptionRepository>();
            services.AddScoped<CustomExceptionFilter>();


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddControllersAsServices();

            services.AddQuartz(options =>
            {
                // base quartz scheduler, job and trigger configuration
                options.UseMemoryStore();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages(async context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized || response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    response.Redirect("/Account/Error");
                }

            });

            app.UseHttpsRedirection();
            app.UseCors("EnableCORS");
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            ApplicationDbContext.CreateRoles(app.ApplicationServices, Configuration).Wait();


            app.UseSignalR(route =>
            {
                route.MapHub<SignalServer>("/SignalServer");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "area",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseQuartz();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var sched = provider.GetRequiredService<IScheduler>();

                var job1 = JobBuilder.Create<QuartzJob1>().WithIdentity("job1").Build();
                if (!sched.CheckExists(job1.Key).Result)
                {
                    //var trigger1 = TriggerBuilder.Create().WithIdentity("trigger1").WithCronSchedule("0 * * ? * *")//For every minute
                    var trigger1 = TriggerBuilder.Create().WithIdentity("trigger1").WithCronSchedule("0 0 23 * * ?")//For every day at 23:00
                        .Build();
                    sched.ScheduleJob(job1, trigger1).GetAwaiter().GetResult();
                }
            }

        }

        private abstract class T
        {
        }
    }
}
