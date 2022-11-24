using MailingApp.Models.Entities;

namespace MailingApp.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(Guid userId);
        Task<User> FindUserByNameAsync(string userName);
        Task<IEnumerable<User>> FindAllUsersAsync();
        Task<IEnumerable<Message>> InboxAsync(User user);
        Task<IEnumerable<Message>> SentMessagesAsync(User user);
    }
}
