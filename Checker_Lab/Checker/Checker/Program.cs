using System;

namespace Checker
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Checker())
                game.Run();
        }
    }
}
