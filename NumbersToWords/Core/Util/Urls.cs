using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace NumbersToWords.Core.Util
{
    public static class Urls
    {
        public static string GetVersionedUrl(string relativePath)
        {
            return string.Format("{0}?v={1}",
                VirtualPathUtility.ToAbsolute(relativePath),
                new FileInfo(HttpContext.Current.Server.MapPath(relativePath)).LastWriteTime.ToString("yyyyMMddHHmm")
            );
        }
    }
}
