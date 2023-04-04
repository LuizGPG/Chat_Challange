using ChatChallange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Service.Interface
{
    public interface IUserChatService
    {
        Task SaveMessage(int userId, string message, string anwser);
        Task<ICollection<UserChat>> GetAllByUserId(int userId);
        UserChat GetUserChatQueue(string user);
    }
}
