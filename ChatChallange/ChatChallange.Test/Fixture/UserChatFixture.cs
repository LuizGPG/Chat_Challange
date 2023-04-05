using ChatChallange.Domain.Entities;
using System.Collections.Generic;

namespace ChatChallange.Test.Fixture
{
    public static class UserChatFixture
    {
        public static UserChat UserChatFix() =>
               new UserChat("User test", "Message test", "Anwser Test");

        public static List<UserChat> UserChatFixtures()
        {
            return new List<UserChat> { UserChatFix() };
        }
    }
}
