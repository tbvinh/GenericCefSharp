using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCefSharp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// http://www.cefsharptutorials.com/Injecting-NET-Object-into-JavaScript-in-CefSharp/
        /// </summary>

        internal static frmMain frm;
        [STAThread]
        static void Main()
        {
            Cef.EnableHighDPISupport();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm = new frmMain();
            Application.Run(frm);
        }
    }
}
