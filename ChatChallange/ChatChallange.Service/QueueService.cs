using ChatChallange.Domain.Entities;
using ChatChallange.Service.Interface;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace ChatChallange.Service
{
    public class QueueService : IQueueService
    {
        private readonly ILogger<QueueService> _logger;

        public QueueService(ILogger<QueueService> logger)
        {
            _logger = logger;
        }

        public bool InsertAnwser(UserChat userChat)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "chatQueue_" + userChat.User,
                                                     durable: false,
                                                     exclusive: false,
                                                     autoDelete: false,
                                                     arguments: null);

                    var message = JsonSerializer.Serialize(userChat);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "chatQueue_" + userChat.User, basicProperties: null, body: body);
                    return true;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying InsertAnwser", ex.Message);
                throw;
            }

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
