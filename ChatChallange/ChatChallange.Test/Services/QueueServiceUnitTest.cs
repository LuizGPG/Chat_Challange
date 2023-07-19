using ChatChallange.Service;
using ChatChallange.Test.Fixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ChatChallange.Test.Services
{
    public class QueueServiceUnitTest
    {
        private readonly ILogger<PublisherUserChat> _publisherLogger;
        private readonly ILogger<ConsumerUserChat> _consumerLogger;

        public QueueServiceUnitTest()
        {
            _publisherLogger = Substitute.For<ILogger<PublisherUserChat>>();
            _consumerLogger = Substitute.For<ILogger<ConsumerUserChat>>();
        }

        //[Test]
        ////rabbit should be on
        //public void Should_InsertAnwser_and_ConsumeAnwserByUser()
        //{
        //    var userChat = UserChatFixture.UserChatFix();
        //    var publisher = new PublisherUserChat(_publisherLogger);
        //    var consumer = new ConsumerUserChat(_consumerLogger);

        //    var returned = publisher.InsertAnwser(userChat);
        //    Assert.IsTrue(returned);

        //    var returnedFromRabbit = consumer.ConsumeAnwserByUser(userChat.User);

        //    Assert.IsNotNull(returnedFromRabbit);
        //    Assert.AreEqual(returnedFromRabbit.Message, userChat.Message);
        //}
    }
}
