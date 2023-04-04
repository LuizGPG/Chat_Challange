using ChatChallange.Domain.Entities;

namespace ChatChallange.Service.Interface
{
    public interface IQueueService
    {
        bool InsertAnwser(UserChat userChat);
        UserChat ConsumeAnwserByUser(string user);
    }
}
