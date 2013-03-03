using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;

namespace PersonalWebsite.Github
{
    // borrowed from ServiceStack.Text's Use Cases
    // modified by Adam K Dean/Imdsm
    // https://github.com/ServiceStack/ServiceStack.Text/
    
    public class GithubV3ApiGateway
    {
        public const string GithubApiBaseUrl = "https://api.github.com/";

        public string ClientID { get; set; }
        public string ClientSecret { get; set; }

        public T GetJson<T>(string route, params object[] routeArgs)
        {
            string uri = GithubApiBaseUrl + string.Format(route, routeArgs);
            if (!string.IsNullOrEmpty(ClientID) && !string.IsNullOrEmpty(ClientSecret))
                uri += string.Format("?client_id={0}&client_secret={1}", ClientID, ClientSecret);
            return uri.GetJsonFromUrl().FromJson<T>();
        }

        public GithubUser GetUser(string githubUsername)
        {
            return GetJson<GithubUser>("users/{0}", githubUsername);
        }

        public List<GithubRepo> GetUserRepos(string githubUsername)
        {
            return GetJson<List<GithubRepo>>("users/{0}/repos", githubUsername);
        }

        public List<GithubRepo> GetOrgRepos(string githubOrgName)
        {
            return GetJson<List<GithubRepo>>("orgs/{0}/repos", githubOrgName);
        }

        public GithubRepo GetUserRepo(string githubUsername, string projectName)
        {
            return GetJson<GithubRepo>("repos/{0}/{1}", githubUsername, projectName);
        }

        public List<GithubUser> GetUserRepoContributors(string githubUsername, string projectName)
        {
            return GetJson<List<GithubUser>>("repos/{0}/{1}/contributors", githubUsername, projectName);
        }

        public List<GithubUser> GetUserRepoWatchers(string githubUsername, string projectName)
        {
            return GetJson<List<GithubUser>>("repos/{0}/{1}/watchers", githubUsername, projectName);
        }

        public List<GithubRepo> GetReposUserIsWatching(string githubUsername)
        {
            return GetJson<List<GithubRepo>>("users/{0}/watched", githubUsername);
        }

        public List<GithubOrg> GetUserOrgs(string githubUsername)
        {
            return GetJson<List<GithubOrg>>("users/{0}/orgs", githubUsername);
        }

        public List<GithubUser> GetUserFollowers(string githubUsername)
        {
            return GetJson<List<GithubUser>>("users/{0}/followers", githubUsername);
        }

        public List<GithubUser> GetOrgMembers(string githubOrgName)
        {
            return GetJson<List<GithubUser>>("orgs/{0}/members", githubOrgName);
        }

        public List<GithubRepo> GetAllUserAndOrgsReposFor(string githubUsername)
        {
            var map = new Dictionary<int, GithubRepo>();
            GetUserRepos(githubUsername).ForEach(x => map[x.Id] = x);
            GetUserOrgs(githubUsername).ForEach(org =>
                GetOrgRepos(org.Login)
                    .ForEach(repo => map[repo.Id] = repo));

            return map.Values.ToList();
        }
    }
}