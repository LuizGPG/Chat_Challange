using ChatChallange.Service;
using ChatChallange.Test.Fixture;
using NUnit.Framework;

namespace ChatChallange.Test.Services
{
    public class QueueServiceUnitTest
    {
        [Test]
        public void Should_InsertAnwser()
        {
            var userChat = UserChatFixture.UserChatFix();
            var service = new QueueService();

            var returned = service.InsertAnwser(userChat);

            Assert.IsTrue(returned);
        }

        [Test]
        public void Should_ConsumeAnwserByUser()
        {
            var userChat = UserChatFixture.UserChatFix();
            var service = new QueueService();

            var returned = service.ConsumeAnwserByUser(userChat.User);

            Assert.IsNotNull(returned);
            Assert.Equals(returned.Message, userChat.Message);
        }
    }
}
