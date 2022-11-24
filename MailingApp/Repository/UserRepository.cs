using MailingApp.DataContext;
using MailingApp.Models.Entities;
using MailingApp.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MailingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MessageAppDbContext _dbContext;

        public UserRepository(MessageAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> FindAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> FindUserByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> FindUserByNameAsync(string userName)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Name == userName);
            if (user is null)
            {
                user = new User { Name = userName.Trim().ToLower() };
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task<IEnumerable<Message>> InboxAsync(User user)
        {
            return await _dbContext.Messages.Where(m => m.To.Id == user.Id).ToListAsync();
        }

        public async Task<IEnumerable<Message>> SentMessagesAsync(User user)
        {
            return await _dbContext.Messages.Where(m => m.From.Id == user.Id).ToListAsync();
        }
    }
}
