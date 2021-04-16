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

            string page;// = string.Format("{0}HTMLResources/index.html", GetAppLocation());
            page = SqliteUtil.config[Enum.GetName(typeof(SqliteUtil.eSetting), SqliteUtil.eSetting.html)];
            return page;
        }
        public static string getInjectJS()
        {
            string url = SqliteUtil.config[Enum.GetName(typeof(SqliteUtil.eSetting), SqliteUtil.eSetting.js)];
            return url;
        }
    }
}
