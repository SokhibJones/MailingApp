using MailingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailingApp.DataContext
{
    public class MessageAppDbContext : DbContext
    {
        public MessageAppDbContext(DbContextOptions<MessageAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
