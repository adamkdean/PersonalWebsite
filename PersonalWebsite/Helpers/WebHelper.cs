using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PersonalWebsite.Helpers
{
    public static class WebHelper
    {
        public static string StripTags(string text)
        {
            return Regex.Replace(text, "<.*?>", string.Empty);
        }
    }
}