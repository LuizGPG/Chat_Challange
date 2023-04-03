using ChatChallange.Hubs;
using ChatChallange.Repository;
using ChatChallange.Repository.Interface;
using ChatChallange.Service;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ChatChallange
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) { 
            services.AddSignalR();

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
