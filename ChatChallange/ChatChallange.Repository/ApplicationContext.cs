using ChatChallange.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatChallange.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        { }

        public DbSet<UserChat> UsersChat { get; set; }

    }
}
