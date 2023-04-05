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
    }
}
