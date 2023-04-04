using ChatChallange.Hubs;
using ChatChallange.Repository;
using ChatChallange.Repository.Interface;
using ChatChallange.Service;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web.UI;

namespace ChatChallange
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
              .EnableSensitiveDataLogging(true));

            services.AddDbContext<IdentityContext>(options =>
              options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"))
              .EnableSensitiveDataLogging(true));


            IocConfig(services);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>();
            
            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages()
                .AddMvcOptions(options => { })
                .AddMicrosoftIdentityUI();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapControllers();

            });
        }

        private static void IocConfig(IServiceCollection services)
        {
            // services
            services.AddScoped<IStooqService, StooqService>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IUserChatService, UserChatService>();

            // repos
            services.AddScoped<IUserChatRepository, UserChatRepository>();
        }
    }
}
