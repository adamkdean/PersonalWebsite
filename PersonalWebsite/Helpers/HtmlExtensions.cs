using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MarkdownSharp;

namespace PersonalWebsite.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Markdown(this HtmlHelper htmlHelper, string text)
        {
            Markdown markdown = new Markdown();
            string html = markdown.Transform(text);            
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString Nl2P(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create(text);
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                builder.Append("<p>");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0) builder.Append("</p><p>");
                    builder.Append(HttpUtility.HtmlEncode(lines[i]));
                }
                builder.Append("</p>");
                return MvcHtmlString.Create(builder.ToString());
            }
        }

        public static MvcHtmlString Nl2Br(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text)) 
                return MvcHtmlString.Create(text);
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0) builder.Append("<br>");
                    builder.Append(HttpUtility.HtmlEncode(lines[i]));
                }
                return MvcHtmlString.Create(builder.ToString());
            }
        }
    }
}