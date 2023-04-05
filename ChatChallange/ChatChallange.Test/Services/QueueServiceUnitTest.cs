using ChatChallange.Service;
using ChatChallange.Test.Fixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;

namespace ChatChallange.Test.Services
{
    public class QueueServiceUnitTest
    {
        private readonly ILogger<QueueService> _logger;

        public QueueServiceUnitTest()
        {
            _logger = Substitute.For<ILogger<QueueService>>();
        }
        [Test]
        public void Should_InsertAnwser()
        {
            var userChat = UserChatFixture.UserChatFix();
            var service = new QueueService(_logger);

            var returned = service.InsertAnwser(userChat);

            Assert.IsTrue(returned);
        }

        [Test]
        public void Should_ConsumeAnwserByUser()
        {
            var userChat = UserChatFixture.UserChatFix();
            var service = new QueueService(_logger);

            var returned = service.ConsumeAnwserByUser(userChat.User);

            Assert.IsNotNull(returned);
            Assert.AreEqual(returned.Message, userChat.Message);
        }
    }
}
