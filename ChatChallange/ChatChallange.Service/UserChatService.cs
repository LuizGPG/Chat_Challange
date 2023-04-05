using ChatChallange.Domain.Entities;
using ChatChallange.Repository.Interface;
using ChatChallange.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatChallange.Service
{
    public class UserChatService : IUserChatService
    {
        private readonly IQueueService _queueService;
        private readonly IUserChatRepository _userChatRepository;
        private readonly ILogger<UserChatService> _logger;

        public UserChatService(IUserChatRepository userChatRepository, IQueueService queueService, ILogger<UserChatService> logger)
        {
            _userChatRepository = userChatRepository;
            _queueService = queueService;
            _logger = logger;
        }

        public async Task<ICollection<UserChat>> GetAll()
        {
            try
            {
                return await _userChatRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying GetAll", ex.Message);
                throw;
            }
        }

        public UserChat GetUserChatQueue(string user)
        {
            var userChat = _queueService.ConsumeAnwserByUser(user);
            return userChat;
        }

        public async Task SaveMessage(UserChat userChat)
        {
            try
            {
                await _userChatRepository.SaveChat(userChat);

                if (userChat.Anwser != string.Empty)
                {
                    _queueService.InsertAnwser(userChat);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying SaveMessage", ex.Message);
                throw;
            }
        }
    }
}
