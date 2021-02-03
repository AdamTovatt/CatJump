using System;

namespace CatJump
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CatJumpGame())
                game.Run();
        }
    }
}
