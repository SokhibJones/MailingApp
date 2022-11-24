using MailingApp.DataContext;
using MailingApp.Models.Entities;
using MailingApp.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MailingApp.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageAppDbContext _dbContext;

        public MessageRepository(MessageAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Message> FindMessageByIdAsync(Guid messageId)
        {
            var result = await _dbContext.Messages.Include(m => m.From).Include(m => m.To).ToListAsync();
            return result.FirstOrDefault(m => m.Id == messageId);
        }
    }
}
