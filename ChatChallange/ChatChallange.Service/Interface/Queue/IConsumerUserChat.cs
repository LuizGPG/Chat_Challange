using ChatChallange.Domain.Entities;

namespace ChatChallange.Service.Interface
{
    public interface IConsumerUserChat
    {
        UserChat ConsumeAnwserByUser(string user);

    }
}
