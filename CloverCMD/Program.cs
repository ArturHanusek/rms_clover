using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloverRMS
{
    static class Program
    {
        static int exitCode = 0; //this is unknown code

        public static void ExitApplication(int exitCode)
        {
            Program.exitCode = exitCode;
            Application.Exit();
        }

        public static void ExitSuccess()
        {
            ExitApplication(1);
        }

        public static void ExitFailed(int errorCode = 1)
        {
            ExitApplication(1000+errorCode);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            return exitCode;
        }
    }
}
