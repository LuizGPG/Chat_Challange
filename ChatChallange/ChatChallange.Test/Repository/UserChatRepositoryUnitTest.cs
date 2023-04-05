using ChatChallange.Domain.Entities;
using ChatChallange.Repository;
using ChatChallange.Test.Fixture;
using Microsoft.EntityFrameworkCore;
using MockQueryable.FakeItEasy;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Test.Repository
{
    public class UserChatRepositoryUnitTest
    {

        [Test]
        public async Task Should_GetAll()
        {
            var mock = UserChatFixture.UserChatFixtures();
            var data = mock.AsQueryable().BuildMockDbSet();

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"Teste_{Guid.NewGuid()}")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                var mockSet = new Mock<DbSet<UserChat>>();
                Setup(data, mockSet);

                var repository = new UserChatRepository(context);

                await repository.SaveChat(mock.First());
                var returned = repository.GetAll();

                Assert.NotNull(returned);
            }
        }

        private static void Setup(IQueryable<UserChat> data, Mock<DbSet<UserChat>> mockSet)
        {
            mockSet.As<IQueryable<UserChat>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserChat>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserChat>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserChat>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        }
    }
}
