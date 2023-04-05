using ChatChallange.Domain.Entities;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class ChatHub : Hub
    {
        private const string Command = "/stock=";
        private const string Method = "ReceiveMessage";

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

                var allMessages = new StringBuilder();
                foreach (var userChat in userChats.OrderBy(d => d.Data))
                {
                    allMessages.Append(CreateListOfMessages(userChat));
                }

                await Clients.All.SendAsync("ReceiveAllMessages", allMessages.ToString());
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


        private string CreateListOfMessages(UserChat userChat)
        {
            var formatData = userChat.Data.ToString("HH:mm MM/dd/yyyy");
            var userMessage = FormatUserMessage(userChat, formatData);
            var anwser = "";
            if (userChat.Anwser != string.Empty)
            {
                anwser = FormatMessageToChatBot(userChat, formatData);
            }

            return userMessage + anwser;
        }


        private async Task SendMessageToChat(UserChat userChat)
        {
            var formatData = userChat.Data.ToString("HH:mm MM/dd/yyyy");
            var userMessage = FormatUserMessage(userChat, formatData);
            await Clients.All.SendAsync(Method, userMessage);
            
            if (userChat.Anwser != string.Empty) {
                var message = FormatMessageToChatBot(userChat, formatData);
                await Clients.All.SendAsync(Method, message);
            }
        }
        private string FormatUserMessage(UserChat userChat, string formatData)
        {
            return "<strong>" + userChat.User + ": </strong>" + userChat.Message + " - " + "<i style='font-size:12px'>" + formatData + "</i>" + "<br />";
        }

        private string FormatMessageToChatBot(UserChat userChat, string formatData)
        {
            var message = "<font color=green> <b> ChatBot: </b></font>" + userChat.Anwser + " - " + "<i style='font-size:12px'>" + formatData + "</i>" + "<br />";

            return message;

        }
    }
}
