using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Enums;
using Moodle.Application.Exceptions;

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
                Console.Clear();
                Console.WriteLine("=== Privatni chat ===");
                Console.WriteLine("1. Nova poruka");
                Console.WriteLine("2. Moji razgovori");
                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await NewMessageAsync();
                        break;
                    case "2":
                        await ShowConversationsAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task NewMessageAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Nova poruka ===");

            var allUsers = await _userService.GetUsersByRoleAsync(UserRole.Student);
            allUsers.AddRange(await _userService.GetUsersByRoleAsync(UserRole.Professor));
            allUsers.RemoveAll(u => u.Id == _currentUserId);
            if (!allUsers.Any())
            {
                Console.WriteLine("Nema korisnika za slanje poruke.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < allUsers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allUsers[i].Email} ({allUsers[i].Role})");
            }

            Console.WriteLine("0. Izlaz");

            Console.Write("Odaberi korisnika: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index > allUsers.Count)
            {
                Console.WriteLine("Nevažeći odabir.");
                Console.ReadKey();
                return;
            }
            else if (index == 0)
            {
                Console.WriteLine("Izlaz...");
                Console.ReadKey();
                return;
            }
                var receiver = allUsers[index - 1];
            Console.Write("Unesi poruku: ");
            var content = Console.ReadLine();

            try
            {
                await _messageService.SendMessageAsync(_currentUserId, receiver.Id, content);
                Console.WriteLine("Poruka poslana!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Neuspjeh pri slanju poruke:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }

            Console.ReadKey();
        }

        private async Task ShowConversationsAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Moji razgovori ===");

            var conversationUserIds = await _messageService.GetConversationUserIdsAsync(_currentUserId);
            if (!conversationUserIds.Any())
            {
                Console.WriteLine("Nema razgovora.");
                Console.ReadKey();
                return;
            }

            var users = new List<User>();
            foreach (var id in conversationUserIds)
            {
                var user = await _userService.GetUsersByRoleAsync(UserRole.Student);
                user.AddRange(await _userService.GetUsersByRoleAsync(UserRole.Professor));
                var u = user.FirstOrDefault(x => x.Id == id);
                if (u != null) users.Add(u);
            }

            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {users[i].Email} ({users[i].Role})");
            }

            Console.Write("Odaberi razgovor: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > users.Count)
            {
                Console.WriteLine("Nevažeći odabir.");
                Console.ReadKey();
                return;
            }

            var chatUser = users[index - 1];
            await ShowChatAsync(chatUser.Id, chatUser.Email);
        }

        private async Task ShowChatAsync(int otherUserId, string otherUserEmail)
        {
            while (true)
            {
                Console.Clear();
                var messages = await _messageService.GetConversationAsync(_currentUserId, otherUserId);
                Console.WriteLine($"=== Chat s {otherUserEmail} ===");
                foreach (var msg in messages)
                {
                    var sender = msg.SenderId == _currentUserId ? "Ja" : otherUserEmail;
                    Console.WriteLine($"{msg.SentAt:dd.MM.yyyy HH:mm} [{sender}]: {msg.Content}");
                }

                Console.WriteLine("\nUpiši poruku (/exit za povratak):");
                var input = Console.ReadLine();
                if (input.Trim().ToLower() == "/exit") break;

                try
                {
                    await _messageService.SendMessageAsync(_currentUserId, otherUserId, input);
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine("Neuspjeh pri slanju poruke:");
                    foreach (var err in ex.Errors)
                        Console.WriteLine("- " + err.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}

