using System.Collections.Generic;

namespace PersonalWebsite.Models.Profile
{
    public class StackOverflowViewModel
    {
        public dynamic Profile { get; set; }
        public dynamic Questions { get; set; }
        public dynamic Answers { get; set; }

        public string Exception { get; set; }        
    }
}