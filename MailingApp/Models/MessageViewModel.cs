using MailingApp.Models.Entities;

namespace MailingApp.Models
{
    public class MessageViewModel
    {
        public User Sender { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Message> InboxMessages { get; set; }
        public IEnumerable<Message> SentMessages { get; set; }
    }
}
