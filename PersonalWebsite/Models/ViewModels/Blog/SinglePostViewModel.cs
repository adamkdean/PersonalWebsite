using System.Collections.Generic;

namespace PersonalWebsite.Models.Blog
{
    public class SinglePostViewModel
    {
        public BlogPost BlogPost { get; set; }
        public bool ShowComments { get; set; }

        public SinglePostViewModel()
        {
            ShowComments = false;
        }
    }
}