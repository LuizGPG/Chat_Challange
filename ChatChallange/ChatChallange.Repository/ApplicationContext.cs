using ChatChallange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ChatChallange.Repository
{
    [ExcludeFromCodeCoverage]
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        { }

        public DbSet<UserChat> UsersChat { get; set; }

    }
}
