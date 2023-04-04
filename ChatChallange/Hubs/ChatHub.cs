using ChatChallange.Domain.Model;
using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IStooqService _stooqService;
        private readonly IUserChatService _userChatService;
        //private readonly IDictionary<string, UserConnection> _connections;

        public ChatHub(IStooqService stooqService, IUserChatService userChatService/*, IDictionary<string, UserConnection> connections*/)
        {
            _stooqService = stooqService;
            _userChatService = userChatService;
            //_connections = connections;
        }

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        //    {
        //        _connections.Remove(Context.ConnectionId);
        //        Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", "Chat Bot", $"{userConnection.User} has left");
        //        SendUsersConnected(userConnection.Room);
        //    }

        //    return base.OnDisconnectedAsync(exception);
        //}

        //public async Task JoinRoom(UserConnection userConnection)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

        //    _connections[Context.ConnectionId] = userConnection;

        //    await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", "Chat Bot", $"{userConnection.User} has joined {userConnection.Room}");

        //    await SendUsersConnected(userConnection.Room);
        //}

        //public async Task SendMessage(string message)
        //{
        //    if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        //    {
        //        await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", userConnection.User, message);
        //    }
        //}

        //public Task SendUsersConnected(string room)
        //{
        //    var users = _connections.Values
        //        .Where(c => c.Room == room)
        //        .Select(c => c.User);

        //    return Clients.Group(room).SendAsync("UsersInRoom", users);
        //}


        public async Task SendMessage(string user, string mensagem)
        {
            var anwser = await _stooqService.CallEndpointStooq(mensagem);
            await _userChatService.SaveMessage(1 /*USER ID*/,mensagem, anwser);

            await GetUserChatQueue(user);
        }

        private async Task GetUserChatQueue(string user)
        {
            var userChat = _userChatService.GetUserChatQueue(user);
            await Clients.All.SendAsync("ReceiveMessage", "User", userChat.Message);
            await Clients.All.SendAsync("ReceiveMessage", "Chat Bot", userChat.Anwser);
            await Clients.User(user).SendAsync("ReceiveMessage", "Chat Bot", userChat.Anwser);
        }

        //fazer metodo que fica escutando fila e valida se a mensagem foi para o usuario em questao

        public async Task LoadChat(string usuario)
        {
            var messages = await _userChatService.GetAllByUserId(1);

            foreach (var message in messages)
            {
                await Clients.All.SendAsync("ReceiveMessage", "User", message.Message);
                await Clients.All.SendAsync("ReceiveMessage", "Chat Bot", message.Anwser);
            }
        }
    }
}
