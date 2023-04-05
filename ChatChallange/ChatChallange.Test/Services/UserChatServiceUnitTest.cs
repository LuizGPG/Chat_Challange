using ChatChallange.Domain.Entities;
using ChatChallange.Repository.Interface;
using ChatChallange.Service;
using ChatChallange.Service.Interface;
using ChatChallange.Test.Fixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatChallange.Test.Services
{
    public class UserChatServiceUnitTest
    {
        private readonly IQueueService _queueService;
        private readonly IUserChatRepository _userChatRepository;
        private readonly ILogger<UserChatService> _logger;

        public UserChatServiceUnitTest()
        {
            _queueService = Substitute.For<IQueueService>();
            _userChatRepository = Substitute.For<IUserChatRepository>();
            _logger = Substitute.For<ILogger<UserChatService>>();
        }

        [Test]
        public void Should_GetAll()
        {
            var usersChatsMock = UserChatFixture.UserChatFixtures();
            _userChatRepository.GetAll().Returns(usersChatsMock);

            var service = new UserChatService(_userChatRepository, _queueService, _logger);
            var usersChats = service.GetAll();

            Assert.IsNotNull(usersChats);
        }

        [Test]
        public void Should_GetAll_Exception()
        {
            _userChatRepository.GetAll().Returns(Task.FromException<ICollection<UserChat>>(new System.Exception()));

            var service = new UserChatService(_userChatRepository, _queueService, _logger);
            var usersChats = service.GetAll();

            Assert.IsNotNull(usersChats.Exception);
        }

        [Test]
        public void Should_GetUserChatQueue()
        {
            var userChatMock = UserChatFixture.UserChatFix();
            _queueService.ConsumeAnwserByUser(userChatMock.User).Returns(userChatMock);

            var service = new UserChatService(_userChatRepository, _queueService, _logger);
            var userChat = service.GetUserChatQueue(userChatMock.User);

            Assert.IsNotNull(userChat);
        }

        [Test]
        public void Should_SaveMessage()
        {
            var userChatMock = UserChatFixture.UserChatFix();
            _userChatRepository.SaveChat(userChatMock).Returns(Task.FromResult);
            _queueService.InsertAnwser(userChatMock).Returns(true);

            var service = new UserChatService(_userChatRepository, _queueService, _logger);
            var userChat = service.SaveMessage(userChatMock);

            Assert.IsNotNull(userChat);
        }

        [Test]
        public void Should_SaveMessage_Exception()
        {
            var userChatMock = UserChatFixture.UserChatFix();
            _userChatRepository.SaveChat(userChatMock).Returns(Task.FromException(new System.Exception()));
            _queueService.InsertAnwser(userChatMock).Returns(true);

            var service = new UserChatService(_userChatRepository, _queueService, _logger);
            var userChat = service.SaveMessage(userChatMock);

            Assert.IsNotNull(userChat.Exception);
        }
    }
}
