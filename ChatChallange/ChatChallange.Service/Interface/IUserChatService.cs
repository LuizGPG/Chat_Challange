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
        Task SaveMessage(string user, string message, string anwser);
        Task<ICollection<UserChat>> GetAll();
        UserChat GetUserChatQueue(string user);
    }
}
