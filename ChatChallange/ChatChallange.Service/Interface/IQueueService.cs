using ChatChallange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Service.Interface
{
    public interface IQueueService
    {
        bool InsertAnwser(UserChat userChat);
        UserChat ConsumeAnwserByUser(string userId);
    }
}
