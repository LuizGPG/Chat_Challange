using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class ChatHub : Hub
    {
        private const string Command = "/stock=";
        private readonly IStooqService _stooqService;
        private readonly IUserChatService _userChatService;

        public ChatHub(IStooqService stooqService, IUserChatService userChatService)
        {
            _stooqService = stooqService;
            _userChatService = userChatService;
        }

        public async Task SendMessage(string user, string message)
        {
            var anwser = string.Empty;
            if (message.Contains(Command))
            {
                message = message.Substring(Command.Length);
                anwser = await _stooqService.CallEndpointStooq(message);
            }
            await _userChatService.SaveMessage(user, message, anwser);

            await GetUserChatQueue(user);
        }

        private async Task GetUserChatQueue(string user)
        {
            var userChat = _userChatService.GetUserChatQueue(user);
            await Clients.All.SendAsync("ReceiveMessage", "User", userChat.Message);
            await Clients.All.SendAsync("ReceiveMessage", "Chat Bot", userChat.Anwser);
            await Clients.User(user).SendAsync("ReceiveMessage", "Chat Bot", userChat.Anwser);
        }


        public async Task LoadChat()
        {
            var messages = await _userChatService.GetAll();

            foreach (var message in messages)
            {
                await Clients.All.SendAsync("ReceiveMessage", "User", message.Message);
                await Clients.All.SendAsync("ReceiveMessage", "Chat Bot", message.Anwser);
            }
        }
    }
}
