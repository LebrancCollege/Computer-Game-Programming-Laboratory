using System;

namespace Tic_Tac_Toe
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Tic_Tac_Toe())
                game.Run();
        }
    }
}
