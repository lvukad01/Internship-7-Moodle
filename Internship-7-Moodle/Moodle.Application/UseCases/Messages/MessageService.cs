using Moodle.Application.Exceptions;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence;

namespace Moodle.Application.UseCases.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SendMessageAsync(int senderId, int receiverId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ValidationException(new[] { ValidationItems.Message.ContentRequired });


            if (content.Length > 1000)
                throw new ValidationException(new[] { ValidationItems.Message.ContentTooLong });


            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.AddAsync(message);
        }

        public async Task<List<Message>> GetConversationAsync(int user1Id, int user2Id)
        {
            return await _messageRepository.GetConversationAsync(user1Id, user2Id);
        }

        public async Task<List<int>> GetConversationUserIdsAsync(int userId)
        {
            return await _messageRepository.GetConversationUserIdsAsync(userId);
        }

        public async Task DeleteUserMessagesAsync(int userId)
        {
            await _messageRepository.DeleteByUserIdAsync(userId);
        }
    }
}
