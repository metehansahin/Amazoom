using System;

namespace Amazoom
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Loads the window application
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new ManagerWindow(args));
        }
    }
}
