using Moodle.Domain.Entities;

namespace Moodle.Application.UseCases.Messages
{
    public interface IMessageService
    {
        Task SendMessageAsync(int senderId, int receiverId, string content);
        Task<List<Message>> GetConversationAsync(int user1Id, int user2Id);
        Task<List<int>> GetConversationUserIdsAsync(int userId);
        Task DeleteUserMessagesAsync(int userId);
    }
}
