using ChatChallange.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatChallange.Repository.Interface
{
    public interface IUserChatRepository
    {
        Task SaveChat(UserChat userChat);
        Task<ICollection<UserChat>> GetAll();
    }
}
