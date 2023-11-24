using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Misc
{
    public class My
    {
        public static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static NLog.Logger NetLogger = NLog.LogManager.GetLogger("MyLog.Net");
    }

    public static class MyExt
    {
        public static string ToLocalFilePath(this Uri uri)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return uri.LocalPath.TrimStart('/');
            }
            else
            {
                return uri.LocalPath;
            }
        }
    }
}
