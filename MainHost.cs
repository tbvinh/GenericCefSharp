using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestCefSharp;

namespace GenericCefSharp
{
    public enum ECommand
    {
        showAbout,
        showMessage
    }
    public class MainHost
    {
        [CefSharp.JavascriptIgnore]
        public ChromiumWebBrowser m_chromeBrowser { get; set; }
        private Type _type;
        
        private Dictionary<ECommand, string> mMethod = new Dictionary<ECommand, string>();

        public MainHost(ChromiumWebBrowser webBrwsr)
        {
            m_chromeBrowser = webBrwsr;
            this._type = this.GetType();

            mMethod.Add(ECommand.showAbout, "showAbout");
            mMethod.Add(ECommand.showMessage, "showMessage");
        }

        public void command(params String[] arg)
        {
            if (arg.Length == 0)
            {
                Debug.Assert(false);
                return;
            }
            String cmd = arg[0];
            try
            {
                ECommand key = (ECommand)Enum.Parse(typeof(ECommand), cmd);
                switch (key)
                {
                    case ECommand.showAbout:

                        this.showAbout();

                        break;

                    case ECommand.showMessage:

                        this.ShowMessage(arg[1]);

                        break;

                    default:

                        MessageBox.Show("Not implemented cmd: " +cmd);

                        break;
                }
            }
            catch
            {
                MessageBox.Show("Not known cmd: " + cmd);

                Debug.Assert(false);
            }

        }
        //private String ExecuteCommand(String methodName, String [] Param)
        //{
        //    String sRet = "";

        //    if (_type != null)
        //    {
        //        MethodInfo methodInfo = _type.GetMethod(methodName);
        //        if (methodInfo != null)
        //        {
        //            object result = methodInfo.Invoke(methodInfo, Param);
        //            sRet = (result != null) ? result.ToString() : "";
        //        }
        //        else
        //        {
        //            Debug.Assert(false);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Assert(false);
        //    }
            

        //    return sRet;
        //}

        public void showAbout()
        {
            Program.frm.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("Phần mềm bán hàng 3.0", "About");
            });
        }

        public void ShowMessage(string message)
        {
            Program.frm.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show(Program.frm, message, "Show Message");
            });
        }

        
    }
}
