using ChatChallange.Domain.Entities;
using ChatChallange.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ICollection<UserChat>> GetAllByUserId(int userId)
        {
            var messages =  await _applicationContext.UsersChat.Where(d => d.UserId == userId).ToListAsync();
            return messages.OrderByDescending(d => d.Data).ToList();
        }

        public async Task SaveChat(UserChat userChat)
        {
            _applicationContext.Add(userChat);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
