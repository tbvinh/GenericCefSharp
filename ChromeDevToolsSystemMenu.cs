using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericCefSharp
{
    static class ChromeDevToolsSystemMenu
    {
        // P/Invoke constants
        public static int WM_SYSCOMMAND = 0x112;
        public static int MF_STRING = 0x0;
        public static int MF_SEPARATOR = 0x800;

        public static int MOD_ALT = 0x0001;
        public static int MOD_CONTROL     =0x0002;
        public static int MOD_SHIFT       =0x0004;
        public static int MOD_WIN         =0x0008;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(IntPtr hwnd, int id);


        // ID for the Chrome dev tools item on the system menu
        public static int SYSMENU_CHROME_DEV_TOOLS = 0x1;
        public static int SYSMENU_CHROME_HOME = 0x2;
        public static int SYSMENU_TOOL_BAR = 0x3;

        public static void CreateSysMenu(Form frm)
        {
            // in your form override the OnHandleCreated function and call this method e.g:
            // protected override void OnHandleCreated(EventArgs e)
            // {
            //     ChromeDevToolsSystemMenu.CreateSysMenu(frm,e);
            // }

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(frm.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
#if DEBUG
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_CHROME_DEV_TOOLS, "&Chrome Dev Tools\tCtrl+F12");
#endif

            AppendMenu(hSysMenu, MF_STRING, SYSMENU_CHROME_HOME, "&Home\tAlt+Home");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_TOOL_BAR, "&Toolbar");
        }

        //RegisterGlobalHotKey(form, Keys.H, MOD_ALT | MOD_SHIFT);

        public static void RegisterGlobalHotKey(Form frm, Keys hotkey, int modifiers)
        {
            try
            {
                // increment the hot key value - we are just identifying
                // them with a sequential number since we have multiples
                mHotKeyId++;

                if (mHotKeyId > 0)
                {
                    // register the hot key combination
                    if (RegisterHotKey(frm.Handle, mHotKeyId, modifiers, Convert.ToInt16(hotkey)) == 0)
                    {
                        // tell the user which combination failed to register -
                        // this is useful to you, not an end user; the end user
                        // should never see this application run
                        MessageBox.Show("Error: " + mHotKeyId.ToString() + " - " +
                            Marshal.GetLastWin32Error().ToString(),
                            "Hot Key Registration");
                    }
                }
            }
            catch
            {
                // clean up if hotkey registration failed -
                // nothing works if it fails
                UnregisterGlobalHotKey(frm);
            }
        }

        private static int mHotKeyId = 0;

        public static void UnregisterGlobalHotKey(Form frm)
        {
            // loop through each hotkey id and
            // disable it
            for (int i = 0; i < mHotKeyId; i++)
            {
                UnregisterHotKey(frm.Handle, i);
            }
        }
    }
}
