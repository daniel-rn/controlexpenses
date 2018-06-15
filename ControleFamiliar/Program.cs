using System;
using System.Windows.Forms;

namespace ControleFamiliar
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }


    }
}
