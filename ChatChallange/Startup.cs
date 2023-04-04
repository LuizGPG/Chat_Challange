using ChatChallange.Hubs;
using ChatChallange.Repository;
using ChatChallange.Repository.Interface;
using ChatChallange.Service;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatChallange
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) { 
            services.AddSignalR();

            services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
              .EnableSensitiveDataLogging(true));
            // services
            services.AddScoped<IStooqService, StooqService>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IUserChatService, UserChatService>();

            // repos
            services.AddScoped<IUserChatRepository, UserChatRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<Chat>("/chat");
            });
        }
    }
}
