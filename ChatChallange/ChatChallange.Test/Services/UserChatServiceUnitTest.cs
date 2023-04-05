using ChatChallange.Repository.Interface;
using ChatChallange.Service;
using ChatChallange.Service.Interface;
using ChatChallange.Test.Fixture;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Test.Services
{
    public class UserChatServiceUnitTest
    {
        private readonly IQueueService _queueService;
        private readonly IUserChatRepository _userChatRepository;

        public UserChatServiceUnitTest()
        {
            _queueService = Substitute.For<IQueueService>();
            _userChatRepository = Substitute.For<IUserChatRepository>();
        }

        [Test]
        public void Should_GetAll()
        {
            var usersChatsMock = UserChatFixture.UserChatFixtures();
            _userChatRepository.GetAll().Returns(usersChatsMock);

            var service = new UserChatService(_userChatRepository, _queueService);
            var usersChats = service.GetAll();

            Assert.IsNotNull(usersChats);
        }

        [Test]
        public void Should_GetUserChatQueue()
        {
            var userChatMock = UserChatFixture.UserChatFix();
            _queueService.ConsumeAnwserByUser(userChatMock.User).Returns(userChatMock);

            var service = new UserChatService(_userChatRepository, _queueService);
            var userChat = service.GetUserChatQueue(userChatMock.User);

            Assert.IsNotNull(userChat);
        }

        [Test]
        public void Should_SaveMessage()
        {
            var userChatMock = UserChatFixture.UserChatFix();
            _userChatRepository.SaveChat(userChatMock).Returns(Task.FromResult);
            _queueService.InsertAnwser(userChatMock).Returns(true);

            var service = new UserChatService(_userChatRepository, _queueService);
            var userChat = service.SaveMessage(userChatMock);

            Assert.IsNotNull(userChat);
        }
    }
}
