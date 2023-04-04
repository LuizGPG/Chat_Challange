using ChatChallange.Domain.Entities;
using ChatChallange.Repository.Interface;
using ChatChallange.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatChallange.Service
{
    public class UserChatService : IUserChatService
    {
        private readonly IQueueService _queueService;
        private readonly IUserChatRepository _userChatRepository;

        public UserChatService(IUserChatRepository userChatRepository, IQueueService queueService)
        {
            _userChatRepository = userChatRepository;
            _queueService = queueService;
        }

        public async Task<ICollection<UserChat>> GetAllByUserId(int userId)
        {
            return await _userChatRepository.GetAllByUserId(userId);
        }

        public UserChat GetUserChatQueue(string user)
        {
            var userChat = _queueService.ConsumeAnwserByUser(user);
            return userChat;
        }

        public async Task SaveMessage(int userId, string message, string anwser)
        {
            try
            {
                var userChat = new UserChat(userId, message, anwser);
                await _userChatRepository.SaveChat(userChat);

                _queueService.InsertAnwser(userChat);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
