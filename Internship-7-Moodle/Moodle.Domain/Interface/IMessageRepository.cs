

using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task<List<Message>> GetConversationAsync(int user1Id, int user2Id);
        Task<List<int>> GetConversationUserIdsAsync(int userId);
        Task DeleteByUserIdAsync(int userId);
    }
}
