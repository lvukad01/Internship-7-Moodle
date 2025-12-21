

using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence
{
    public interface IMessageRepository
    {
        Task<Message?> GetByIdAsync(int id);
        Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
        Task AddAsync(Message message);
        void Update(Message message);
        Task DeleteAsync(Message message);
    }
}
