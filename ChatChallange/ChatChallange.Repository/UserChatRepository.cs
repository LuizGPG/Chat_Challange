using ChatChallange.Domain.Entities;
using ChatChallange.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallange.Repository
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UserChatRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<ICollection<UserChat>> GetAll()
        {
            return await _applicationContext.UsersChat.OrderByDescending(c => c.Data).Take(50).ToListAsync();
        }

        public async Task SaveChat(UserChat userChat)
        {
            _applicationContext.Add(userChat);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
