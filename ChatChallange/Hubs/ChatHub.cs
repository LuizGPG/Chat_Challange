using ChatChallange.Domain.Entities;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class ChatHub : Hub
    {
        private const string Command = "/stock=";
        private const string Method = "ReceiveMessage";
        private const string ChatBot = "Chat Bot";

        private readonly ILogger<ChatHub> _logger;
        private readonly IStooqService _stooqService;
        private readonly IUserChatService _userChatService;

        public ChatHub(IStooqService stooqService, IUserChatService userChatService, ILogger<ChatHub> logger)
        {
            _stooqService = stooqService;
            _userChatService = userChatService;
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("Error trying send message", ex.Message);
            }
        }


        public async Task LoadChat()
        {
            try
            {
                var userChats = await _userChatService.GetAll();

                foreach (var userChat in userChats.OrderBy(d => d.Data))
                {
                    await SendMessageToChat(userChat);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying Load chat", ex.Message);
            }
        }

        private async Task GetUserChatQueue(string user)
        {
            var userChat = _userChatService.GetUserChatQueue(user);
            await SendMessageToChat(userChat);
        }

        private async Task SendMessageToChat(UserChat userChat)
        {
            var formatData = userChat.Data.ToString("HH:mm MM/dd/yyyy");
            await Clients.All.SendAsync(Method, userChat.User, userChat.Message, formatData);
            if (userChat.Anwser != string.Empty)
                await Clients.All.SendAsync(Method, ChatBot, userChat.Anwser, formatData);
        }
    }
}
