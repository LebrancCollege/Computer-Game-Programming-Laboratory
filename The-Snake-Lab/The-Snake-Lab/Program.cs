using System;

namespace The_Snake_Lab
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TheSnake())
                game.Run();
        }
    }
}
