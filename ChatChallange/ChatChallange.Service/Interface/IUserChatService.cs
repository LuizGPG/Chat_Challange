using ChatChallange.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatChallange.Service.Interface
{
    public interface IUserChatService
    {
        Task SaveMessage(UserChat userChat);
        Task<ICollection<UserChat>> GetAll();
        UserChat GetUserChatQueue(string user);
    }
}
