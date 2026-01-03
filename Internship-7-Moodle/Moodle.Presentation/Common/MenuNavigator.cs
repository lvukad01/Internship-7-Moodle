namespace Moodle.Presentation.Common
{
    public static class MenuNavigator
    {
        public static int Show(string title, List<string> options)
        {
            int selectedIndex = 0;
            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine();

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = selectedIndex == 0 ? options.Count - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = selectedIndex == options.Count - 1 ? 0 : selectedIndex + 1;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = true;
                    return -1;
                }

            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            return selectedIndex;
        }
    }
}
