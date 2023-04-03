using ChatChallange.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatChallange.Hubs
{
    public class Chat : Hub
    {
        private readonly IStooqService _stooqService;
        private readonly IUserChatService _userChatService;

        public Chat(IStooqService stooqService, IUserChatService userChatService)
        {
            _stooqService = stooqService;
            _userChatService = userChatService;
        }

        public async Task SendMessage(string usuario, string mensagem)
        {
            var anwser = await _stooqService.CallEndpointStooq(mensagem);
            /*remover apos fazer leitura diretamente da fila*/
            await Clients.All.SendAsync("ReceiveMessage", usuario, mensagem);
            await Clients.All.SendAsync("ReceiveMessage", "Chat Bot", anwser);

            await _userChatService.SaveMessage(1 /*USER ID*/,mensagem, anwser);
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
