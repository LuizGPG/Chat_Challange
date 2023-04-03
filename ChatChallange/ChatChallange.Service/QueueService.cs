using ChatChallange.Domain.Entities;
using ChatChallange.Service.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatChallange.Service
{
    public class QueueService : IQueueService
    {
        public bool InsertAnwser(UserChat userChat)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "chatQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var message = JsonSerializer.Serialize(userChat);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "chatQueue", basicProperties: null, body: body);
                    return true;

                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public UserChat ConsumeAnwserByUser(string userId)
        {
            var user = new UserChat();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "chatQueue", durable: false, exclusive: false,autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        user = JsonSerializer.Deserialize<UserChat>(message);

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        //Logger
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                };
                
                channel.BasicConsume(queue: "chatQueue",
                                     autoAck: false,
                                     consumer: consumer);
            }

            return user;
        }
    }
}
