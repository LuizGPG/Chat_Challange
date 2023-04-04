using System;

namespace ChatChallange.Domain.Entities
{
    public class UserChat
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Data { get; set; }
        public string Message { get; set; }
        public string Anwser { get; set; }

        public UserChat(string user, string message, string anwser)
        {
            User = user;
            Message = message;
            Anwser = anwser;
            Data = DateTime.Now;
        }
    }
}
