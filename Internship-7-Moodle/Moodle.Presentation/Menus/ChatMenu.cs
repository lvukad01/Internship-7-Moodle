using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Enums;
using Moodle.Application.Exceptions;
using Moodle.Presentation.Common;

namespace Moodle.Presentation.Menus
{
    public class ChatMenu
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly int _currentUserId;

        public ChatMenu(IMessageService messageService, IUserService userService, int currentUserId)
        {
            _messageService = messageService;
            _userService = userService;
            _currentUserId = currentUserId;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                var choice = MenuNavigator.Show(
                    "PRIVATNI CHAT",
                    new List<string>
                    {
                        "Nova poruka",
                        "Moji razgovori",
                        "Nazad"
                    });

                if (choice == -1 || choice == 2)
                    return;

                if (choice == 0)
                    await NewMessageAsync();

                if (choice == 1)
                    await ShowConversationsAsync();
            }
        }

        private async Task NewMessageAsync()
        {
            var users = await GetAllChatUsersAsync();
            if (!users.Any())
            {
                Console.WriteLine("Nema korisnika za slanje poruke.");
                Console.ReadKey();
                return;
            }

            var options = users
                .Select(u => $"{u.Email} ({u.Role})")
                .Append("Nazad")
                .ToList();

            var choice = MenuNavigator.Show("NOVA PORUKA", options);

            if (choice == -1 || choice == users.Count)
                return;

            var receiver = users[choice];

            Console.Clear();
            Console.WriteLine($"Poruka za: {receiver.Email}");
            Console.Write("Unesi poruku: ");
            var content = Console.ReadLine();

            try
            {
                await _messageService.SendMessageAsync(_currentUserId, receiver.Id, content);
                Console.WriteLine("Poruka poslana!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Greška:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }

            Console.ReadKey();
        }

        private async Task ShowConversationsAsync()
        {
            var conversationUserIds = await _messageService.GetConversationUserIdsAsync(_currentUserId);
            if (!conversationUserIds.Any())
            {
                Console.WriteLine("Nema razgovora.");
                Console.ReadKey();
                return;
            }

            var users = await GetAllChatUsersAsync();
            var chatUsers = users
                .Where(u => conversationUserIds.Contains(u.Id))
                .ToList();

            var options = chatUsers
                .Select(u => $"{u.Email} ({u.Role})")
                .Append("Nazad")
                .ToList();

            var choice = MenuNavigator.Show(" MOJI RAZGOVORI", options);

            if (choice == -1 || choice == chatUsers.Count)
                return;

            var user = chatUsers[choice];
            await ShowChatAsync(user.Id, user.Email);
        }

        private async Task ShowChatAsync(int otherUserId, string otherUserEmail)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($" Chat s {otherUserEmail}\n");

                var messages = await _messageService.GetConversationAsync(_currentUserId, otherUserId);
                foreach (var msg in messages)
                {
                    var sender = msg.SenderId == _currentUserId ? "Ja" : otherUserEmail;
                    Console.WriteLine($"{msg.SentAt:dd.MM.yyyy HH:mm} [{sender}]: {msg.Content}");
                }

                Console.WriteLine("\nUpiši poruku (/exit za povratak):");
                var input = Console.ReadLine();

                if (input?.Trim().ToLower() == "/exit")
                    return;

                try
                {
                    await _messageService.SendMessageAsync(_currentUserId, otherUserId, input);
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine("Greška:");
                    foreach (var err in ex.Errors)
                        Console.WriteLine("- " + err.Message);
                    Console.ReadKey();
                }
            }
        }

        private async Task<List<User>> GetAllChatUsersAsync()
        {
            var users = await _userService.GetUsersByRoleAsync(UserRole.Student);
            users.AddRange(await _userService.GetUsersByRoleAsync(UserRole.Professor));
            users.AddRange(await _userService.GetUsersByRoleAsync(UserRole.Admin));

            return users
                .Where(u => u.Id != _currentUserId)
                .ToList();
        }
    }
}

