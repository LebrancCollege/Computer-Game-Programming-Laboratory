using System;

namespace Snake
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainScene())
                game.Run();
        }
    }
}
