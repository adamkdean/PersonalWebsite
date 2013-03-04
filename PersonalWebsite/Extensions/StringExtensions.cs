using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MarkdownSharp;
using System.Text.RegularExpressions;

namespace PersonalWebsite.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAccent(this string s)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(s);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string Slugify(this string s, int maxLength = 60)
        {
            s = HttpUtility.HtmlDecode(s).RemoveAccent();
            s = Regex.Replace(s, @"[ ]{2,}", " ").Trim();
            s = Regex.Replace(s, @"[ ]{2,}", " ").Trim();
            s = Regex.Replace(s, "[^a-zA-Z0-9 -]+", " ", RegexOptions.Compiled);
            s = s.Replace(" ", "-").ToLower();
            s = Regex.Replace(s, @"[-]{2,}", "-").Trim();
            while (s.EndsWith("-") && s.Length > 1)
                s = s.Substring(0, s.Length - 1);
            if (s.Length > maxLength) s = s.Substring(0, maxLength);
            return s;
        }
    }
}