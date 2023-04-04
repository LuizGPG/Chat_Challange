using ChatChallange.Domain.Entities;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class ChatHub : Hub
    {
        private const string Command = "/stock=";
        private const string Method = "ReceiveMessage";
        private const string ChatBot = "Chat Bot";

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
            
            var userChat = new UserChat(user, message, anwser);
            await _userChatService.SaveMessage(userChat);

            if (userChat.Anwser != string.Empty)
            {
                await GetUserChatQueue(user);
            }
            else
            {
                await SendMessageToChat(userChat);
            }
        }

        private async Task GetUserChatQueue(string user)
        {
            var userChat = _userChatService.GetUserChatQueue(user);
            await SendMessageToChat(userChat);
        }

        public async Task LoadChat()
        {
            var userChats = await _userChatService.GetAll();

            foreach (var userChat in userChats)
            {
                await SendMessageToChat(userChat);
            }
        }

        private async Task SendMessageToChat(UserChat userChat)
        {
            await Clients.All.SendAsync(Method, userChat.User, userChat.Message);
            if (userChat.Anwser != string.Empty)
                await Clients.All.SendAsync(Method, ChatBot, userChat.Anwser);
        }
    }
}
