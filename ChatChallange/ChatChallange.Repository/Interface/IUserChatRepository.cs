using ChatChallange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Repository.Interface
{
    public interface IUserChatRepository
    {
        Task SaveChat(UserChat userChat);
        Task<ICollection<UserChat>> GetAllByUserId(int userId);
    }
}
