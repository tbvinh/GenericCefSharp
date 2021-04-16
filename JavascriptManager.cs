using CefSharp;
using CefSharp.Handler;
using CefSharp.WinForms;
using GenericCefSharp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GenericCefSharp
{

    public class JavascriptManager
    {
        public static void injetJS(ChromiumWebBrowser browser)
        {
            String const_enum = "let ";
            foreach (ECommand cmd in (ECommand[])Enum.GetValues(typeof(ECommand)))
            {
                const_enum += String.Format("window.{0}={1};", Enum.GetName(typeof(ECommand), cmd), (int)cmd);
            }
            const_enum = const_enum.Substring(0, const_enum.Length - 1)+";"; 

            browser.ExecuteScriptAsync("var sc = document.createElement('script');");
            browser.ExecuteScriptAsync("sc.setAttribute('type', 'text/javascript');");
            browser.ExecuteScriptAsync("sc.text = '"+ const_enum + "  '");
            //browser.ExecuteScriptAsync(script);
            //browser.ExecuteScriptAsync(@"script.src='null'");
            //browser.ExecuteScriptAsync(@"sc.src='" + HtmlPageUtils.getInjectJS().Replace("\\","/") +"'");
            browser.ExecuteScriptAsync("document.body.appendChild(sc)");
            //browser.ExecuteScriptAsync("document.getElementsByTagName('head')[0].appendChild(script)");
        }
    }
}
