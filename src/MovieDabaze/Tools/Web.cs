using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MovieDabaze.Tools
{
    class Web
    {
        private String  _url = "http://google.fr";
        public bool     _is_connected { get; set; }

        public Web()
        {
        }

        public Web(String url)
        {
            _url = url;
        }

        public static String GetPageContent(String url)
        {
            HttpWebResponse httpWResponse = null;
            StreamReader sr = null;
            String ans = null;

            try
            {
                HttpWebRequest httpWRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWResponse = (HttpWebResponse)httpWRequest.GetResponse();
                sr = new StreamReader(httpWResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                ans = sr.ReadToEnd();
            }
            catch
            {
                ans = null;
            }
            finally
            {
                if (httpWResponse != null) httpWResponse.Close();
                if (sr != null) sr.Close();
            }
            return ans;
        }
    }
}
