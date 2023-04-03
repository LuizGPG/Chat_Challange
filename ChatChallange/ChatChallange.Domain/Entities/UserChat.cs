using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Domain.Entities
{
    public class UserChat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Data { get; set; }
        public string Message { get; set; }
        public string Anwser { get; set; }

        public UserChat()
        {

        }
        public UserChat(int userId, string message, string anwser)
        {
            UserId = userId;
            Message = message;
            Anwser = anwser;
            Data = DateTime.Now;
        }
    }
}
