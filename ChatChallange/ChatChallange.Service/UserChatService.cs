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
        private readonly IConsumerUserChat _consumerUserChat;
        private readonly IPublisherUserChat _publisherUserChat;
        private readonly IUserChatRepository _userChatRepository;
        private readonly ILogger<UserChatService> _logger;

        public UserChatService(IUserChatRepository userChatRepository, IConsumerUserChat consumerUserChat, 
            IPublisherUserChat publisherUserChat, ILogger<UserChatService> logger)
        {
            _userChatRepository = userChatRepository;
            _logger = logger;
            _consumerUserChat = consumerUserChat;
            _publisherUserChat = publisherUserChat;
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
            var userChat = _consumerUserChat.ConsumeAnwserByUser(user);
            return userChat;
        }

        public async Task<bool> SaveMessage(UserChat userChat)
        {
            try
            {
                await _userChatRepository.SaveChat(userChat);

                if (userChat.Anwser != string.Empty)
                {
                    _publisherUserChat.InsertAnwser(userChat);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying SaveMessage", ex.Message);
                return false;
                throw;
            }
        }
    }
}
