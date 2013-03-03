using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;

namespace PersonalWebsite.Github
{
    public class GithubOrg
    {
        public int Id { get; set; }
        public string Avatar_Url { get; set; }
        public string Url { get; set; }
        public string Login { get; set; }
    }
}