using CefSharp;
using CefSharp.WinForms;
using GenericCefSharp;
using GenericCefSharp.CefHandler;
using GenericCefSharp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCefSharp
{
    public partial class frmMain : Form
    {
        public ChromiumWebBrowser browser;
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var setting = new CefSettings();
            
            setting.Locale = "vi";
            
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            
            setting.BrowserSubprocessPath = System.IO.Path.GetFullPath( @"x86\CefSharp.BrowserSubprocess.exe");
            //setting.CefCommandLineArgs.Add("disable-gpu", "1");
            //setting.RemoteDebuggingPort = 8088;
            Cef.Initialize(setting, performDependencyCheck: true, browserProcessHandler: null);

            var www = "https://partner.thegioididong.com";
            browser = new ChromiumWebBrowser("")// ChromiumWebBrowser(www)
            {
                Dock = DockStyle.Fill
                
            };

            browser.MenuHandler = new CustomMenuHandler();
            browser.LifeSpanHandler = new LifeSpanHandler();

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.ObjectBoundInJavascript += (sender1, e1) =>
            {
                var name = e1.ObjectName;

                Debug.WriteLine($"Object {e1.ObjectName} was bound successfully.");
                MessageBox.Show(e1.ObjectName);
            };
            browser.JavascriptObjectRepository.Register("dotNetMessage", new GenericCefSharp.MainHost(this.browser), isAsync: true, options: BindingOptions.DefaultBinder);

            browser.IsBrowserInitializedChanged += (sender1, args1) =>
            {
                if (browser.IsBrowserInitialized)
                {
                    browser.Load(HtmlPageUtils.getIndex());
                    //browser.LoadHtml(File.ReadAllText(HtmlPageUtils.getIndex()));
                }
            };

            // Add it to the form and fill it to the form window.
            this.Controls.Add(browser);
            //new Thread(() =>
            //{
                
            //    Thread.Sleep(1000);
            //    Application.DoEvents();

            //    //this.Invoke((MethodInvoker)delegate
            //    //{
            //    //    this.Text = www;
            //    //});

            //    this.InvokeOnUiThreadIfRequired(() => this.Text = www);

                
            //    browser.Load(www);
            //    //this.Invoke((MethodInvoker)delegate
            //    //{
            //    //    browser.Refresh();
            //    //});
               
            //    this.InvokeOnUiThreadIfRequired(() => browser.Refresh());

            //}).Start();


            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;

            ChromeDevToolsSystemMenu.CreateSysMenu(this);

        }
        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == ChromeDevToolsSystemMenu.WM_SYSCOMMAND) && ((int)m.WParam == ChromeDevToolsSystemMenu.SYSMENU_CHROME_DEV_TOOLS))
            {
                browser.ShowDevTools();
            }
        }

    }
}
