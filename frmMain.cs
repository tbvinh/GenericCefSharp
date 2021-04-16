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

            browser = new ChromiumWebBrowser("")// ChromiumWebBrowser(www)
            {
                Dock = DockStyle.Fill
                
            };

            browser.MenuHandler = new CustomMenuHandler();
            browser.LifeSpanHandler = new LifeSpanHandler();

            BrowserSettings browserSettings = new BrowserSettings();

            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            
            browser.BrowserSettings = browserSettings;

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.ObjectBoundInJavascript += (sender1, e1) =>
            {
                var name = e1.ObjectName;

                Debug.WriteLine($"Object {e1.ObjectName} was bound successfully.");
                MessageBox.Show(e1.ObjectName);
            };
            browser.JavascriptObjectRepository.Register("dotNetObject", new GenericCefSharp.MainHost(this.browser), isAsync: true, options: BindingOptions.DefaultBinder);
            SqliteUtil.getSettings();
            browser.IsBrowserInitializedChanged += (sender1, args1) =>
            {
                if (browser.IsBrowserInitialized)
                {
                    goHome();
                    //browser.LoadHtml(File.ReadAllText(HtmlPageUtils.getIndex()));
                }
            };
            
            // Add it to the form and fill it to the form window.
            this.Controls.Add(browser);
            browser.BringToFront();
            
            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            browser.LoadingStateChanged += Browser_LoadingStateChanged;
            ChromeDevToolsSystemMenu.CreateSysMenu(this);

            ChromeDevToolsSystemMenu.RegisterGlobalHotKey(this, Keys.Home, ChromeDevToolsSystemMenu.MOD_ALT);
#if DEBUG
            ChromeDevToolsSystemMenu.RegisterGlobalHotKey(this, Keys.F12, ChromeDevToolsSystemMenu.MOD_CONTROL);
#endif

        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            
            //if (!e.IsLoading)
            //{
            //    if (!homeInject)
            //    {
            //        JavascriptManager.injetJS((ChromiumWebBrowser)sender);
            //        homeInject = true;
            //    }
            //}
            
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
            //b.ExecuteScriptAsync(@"D:\Projects\GenericCefSharp\HTMLResources\dotnet.js");
        }

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            Cef.Shutdown();
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if (m.Msg == ChromeDevToolsSystemMenu.WM_SYSCOMMAND)
            {
                if ((int)m.WParam == ChromeDevToolsSystemMenu.SYSMENU_CHROME_DEV_TOOLS)
                {
                    browser.ShowDevTools();
                }
                else if ((int)m.WParam == ChromeDevToolsSystemMenu.SYSMENU_CHROME_HOME)
                {
                    goHome();
                }
                else if ((int)m.WParam == ChromeDevToolsSystemMenu.SYSMENU_TOOL_BAR)
                {
                    showToolbar();
                }
            }
            else if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == 1)//Ctrl_H
                {
                    goHome();
                }else if (id == 2)//Ctrl_H
                {
                    browser.ShowDevTools();
                }
            }
        }
        void showToolbar()
        {
            this.tsMain.Visible = !this.tsMain.Visible;
        }

        Boolean homeInject = false;
        void goHome()
        {
            homeInject = false;
            browser.Load(HtmlPageUtils.getIndex());
        }

        private void tsSetting_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();
            frm.ShowDialog();
        }

        private void tsHome_Click(object sender, EventArgs e)
        {
            goHome();
        }
    }
}
