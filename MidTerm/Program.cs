using System;

namespace Yew 
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MidTerm.MidTerm())
            {
                game.Run();
            }
        }
    }
}
