using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCefSharp.CefHandler
{
    class LifeSpanHandler : ILifeSpanHandler
    {
        bool ILifeSpanHandler.DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return false;
        }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            
        }

        void ILifeSpanHandler.OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            
        }

        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            //System.Diagnostics.Process.Start(targetUrl);
            newBrowser = null;
            return true;
        }
    }
}
