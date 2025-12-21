

namespace Moodle.Domain.Entities
{
    public class Message
    {
        public int Id { get; private set; }

        public int SenderId { get; private set; }
        public User Sender { get; private set; } = null!;

        public int ReceiverId { get; private set; }
        public User Receiver { get; private set; } = null!;

        public string Content { get; private set; } = null!;
        public DateTime SentAt { get; private set; }
    }

}
