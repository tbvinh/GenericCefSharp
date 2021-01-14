using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestCefSharp;

namespace GenericCefSharp
{
    public class MainHost
    {
        [CefSharp.JavascriptIgnore]
        public ChromiumWebBrowser m_chromeBrowser { get; set; }
        public MainHost(ChromiumWebBrowser webBrwsr)
        {
            m_chromeBrowser = webBrwsr;
        }

        public void showAbout()
        {
            Program.frm.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("Hello Vinh");
            });
        }

        public void Show(string message)
        {
            Program.frm.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show(Program.frm, message);
            });
        }
    }
}
