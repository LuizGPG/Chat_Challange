using ChatChallange.Domain.Entities;

namespace ChatChallange.Service.Interface
{
    public interface IPublisherUserChat
    {
        bool InsertAnwser(UserChat userChat);
    }
}
