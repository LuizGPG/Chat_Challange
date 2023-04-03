using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class Chat : Hub
    {
        public async Task SendMessage(string usuario, string mensagem)
        {
            await Clients.All.SendAsync("ReceiveMessage", usuario, mensagem);
        }
    }
}
