using ChatChallange.Domain.Entities;
using ChatChallange.Service.Interface;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace ChatChallange.Service
{
    public class ConsumerUserChat : IConsumerUserChat
    {
        private readonly ILogger<ConsumerUserChat> _logger;

        public ConsumerUserChat(ILogger<ConsumerUserChat> logger)
        {
            _logger = logger;
        }

        public UserChat ConsumeAnwserByUser(string user)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var data = channel.BasicGet("chatQueue_" + user, true);
                    var body = data.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var userChat = JsonSerializer.Deserialize<UserChat>(message);
                    return userChat;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying ConsumeAnwserByUser", ex.Message);
                throw;
            }
        }
    }
}
