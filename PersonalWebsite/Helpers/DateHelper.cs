using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PersonalWebsite.Helpers
{
    public static class DateHelper
    {
        public static string GetReadableDate(DateTime date)
        {
            return string.Format("{0:dddd, d MMMM yyyy}", date);
        }

        public static string GetMonthName(int month)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            return dtfi.GetMonthName(month);
        }

        public static string GetReadableTimeSince(DateTime originalTime)
        {
            TimeSpan ts = DateTime.Now - originalTime;

            string message = "";

            if (ts.TotalSeconds < 60)
            {
                message = "less than a minute ago";
            }
            else if (ts.TotalMinutes < 3)
            {
                message = "a few minutes ago";
            }
            else if (ts.TotalMinutes < 60)
            {
                message = "less than an hour ago";
            }
            else if (ts.TotalDays < 1 && originalTime.Day == DateTime.Now.Day)
            {
                message = "today";
            }
            else if ((ts.TotalDays < 1 && originalTime.Day < DateTime.Now.Day) ||
                     (ts.TotalDays < 2 && originalTime.Day == DateTime.Now.Day - 1))
            {
                message = "yesterday";
            }
            else if (ts.TotalDays < 7)
            {
                message = "less than a week ago";
            }
            else if (ts.TotalDays < 28)
            {
                message = string.Format("{0} days ago", ts.TotalDays);
            }
            else if (ts.TotalDays < 365)
            {
                int months = (int)(ts.TotalDays / 28);
                message = string.Format("about {0} months ago", months);
            }
            else
            {
                message = "more than a year ago";
            }

            return message;
        }
    }
}