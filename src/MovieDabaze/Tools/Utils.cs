using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieDabaze.Tools
{
    class Utils
    {
        internal static string ConvertToAsciiAnsi(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                int asciiVal = (int)c;
                if (asciiVal > 127) sb.AppendFormat(string.Format("%{0:x2}", asciiVal));
                else sb.Append(c);
            }
            return sb.ToString();
        }

        internal static Match GetStringBetween(string txt, string start, string end, string groupName)
        {
            string pattern = string.Format("({0})(?<{2}>.*?)({1})", start, end, groupName);
            return Regex.Match(txt, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        internal static string DeleteSpace(string htmlText)
        {
            string regex = @"\r\n";
            return Regex.Replace(htmlText, regex, string.Empty, RegexOptions.IgnoreCase);
        }

        internal static string DeleteHtmlTags(string htmlText)
        {
            string regex = @"<(/)?.{1,2}>";
            return Regex.Replace(htmlText, regex, string.Empty, RegexOptions.IgnoreCase);
        }

        internal static int GetDuration(string h, string m)
        {
            if (h == string.Empty) return Int32.Parse(m);
            if (m == string.Empty) return Int32.Parse(h) * 60;
            return Int32.Parse(h) * 60 + Int32.Parse(m);
        }
    }
}
