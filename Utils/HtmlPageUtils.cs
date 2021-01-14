using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCefSharp.Utils
{
    class HtmlPageUtils
    {
        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        public static string getIndex()
        {
            string page = string.Format("{0}HTMLResources/index.html", GetAppLocation());
            return page;
        }
    }
}
