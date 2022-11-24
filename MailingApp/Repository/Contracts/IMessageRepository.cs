using MailingApp.Models.Entities;

namespace MailingApp.Repository.Contracts
{
    public interface IMessageRepository
    {
        Task<Message> FindMessageByIdAsync(Guid messageId);
        Task AddMessageAsync(Message message);
    }
}
