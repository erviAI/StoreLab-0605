namespace StoreLab.RetroStore.Utils
{
    public static class NotificationHelper
    {
        public static void ShowNotification(string message, ConsoleColor color)
        {
            int notificationLine = Console.WindowHeight - 3;
            Console.SetCursorPosition(0, notificationLine);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, notificationLine);
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            if (color == ConsoleColor.Green)
            {
                Thread.Sleep(500);
            }
            else
            {
                Thread.Sleep(1200);
            }
        }
    }
}
