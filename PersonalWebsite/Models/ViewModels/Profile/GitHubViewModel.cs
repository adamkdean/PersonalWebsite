using System.Collections.Generic;
using PersonalWebsite.Github;

namespace PersonalWebsite.Models.Profile
{
    public class GitHubViewModel
    {
        public GithubUser User { get; set; }
        public List<GithubRepo> Repositories { get; set; }

        public string Exception { get; set; }
    }
}