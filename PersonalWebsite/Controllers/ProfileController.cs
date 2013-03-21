using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PersonalWebsite.Classes;
using PersonalWebsite.Models.Profile;
using StackExchange.StacMan;
using PersonalWebsite.Github;
using System;

namespace PersonalWebsite.Controllers
{
    public partial class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public virtual ActionResult Index()
        {            
            return View();
        }

        #region ChildActions
        [ChildActionOnly]
        [OutputCache(Duration = 900, VaryByParam = "none")] // 15 minutes
        public virtual PartialViewResult StackOverflow()
        {
            var model = new StackOverflowViewModel();

            try
            {
                var site = "stackoverflow";
                var id = new List<int>() { 1138620 };
                var client = new StacManClient(key: "B6EuGp0wJiOeWNiAd2za)w((");
                
                var users_response = client.Users.GetByIds(site, id, "!T6p4VHlBGj(I.LUZgV");
                var questions_response = client.Users.GetQuestions(site, id);
                var answers_response = client.Users.GetAnswers(site, id, "!-.mgWLrn264w");

                // default: client.ApiTimeoutMs = 5000;
                users_response.Wait();
                questions_response.Wait();
                answers_response.Wait();

                if (users_response.IsCompleted) model.Profile = users_response.Result.Data.Items[0];                
                if (questions_response.IsCompleted) model.Questions = questions_response.Result.Data.Items.Take(5);
                if (answers_response.IsCompleted) model.Answers = answers_response.Result.Data.Items.Take(5);
                
                if (users_response.Exception != null) model.Exception = users_response.Exception.Message;
                else if (users_response.Exception != null) model.Exception = questions_response.Exception.Message;
                else if (users_response.Exception != null) model.Exception = answers_response.Exception.Message;                
            }
            catch (Exception e)
            {
                model.Exception = e.Message;
            }

            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 900, VaryByParam = "none")] // 15 minutes
        public virtual PartialViewResult GitHub()
        {
            var model = new GitHubViewModel();

            try
            {
                var github = new GithubV3ApiGateway();
                model.User = github.GetUser("imdsm");
                model.Repositories = github.GetUserRepos("imdsm")
                                        .OrderByDescending(x => x.Updated_At)
                                        .Take(5)
                                        .ToList();
            }
            catch (Exception e)
            {
                model.Exception = e.Message;
            }

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult AboutMe()
        {
            var model = new AboutMeViewModel();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Self-taught programmer with experience using a vast array of languages.</p>");
            sb.Append("<p>These days I focus on C# ASP.NET using frameworks such as MVC4 and EF. I have been writing code in C# consistently since 2006, having experience with .NET 2.0 and ASP.NET web forms right up to .NET 4.5 and MVC4.</p>");
            sb.Append("<p>I am also a deft hand at design, having years of experience in hand-coding designs using HTML/CSS and using frameworks such as Bootstrap, Foundation and jQuery as well as design software like Adobe Photoshop, Illustrator and even InDesign for print media.</p>");            
            model.Blurb = sb.ToString();

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult Skills()
        {
            var model = new SkillsViewModel();

            var primarySkills = new List<Skill>();
            primarySkills.Add(new Skill("C#", 90));
            primarySkills.Add(new Skill("ASP.NET", 80));
            primarySkills.Add(new Skill("MVC", 60));
            model.PrimarySkills = primarySkills;

            var secondarySkills = new List<Skill>();
            secondarySkills.Add(new Skill("HTML", 80));
            secondarySkills.Add(new Skill("CSS", 70));
            secondarySkills.Add(new Skill("JS", 50));
            secondarySkills.Add(new Skill("PHP", 60));
            secondarySkills.Add(new Skill("SQL", 70));
            model.SecondarySkills = secondarySkills;

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult Traits()
        {
            var model = new TraitsViewModel();

            var traits = new List<Trait>();
            traits.Add(new Trait("Fast Learner", "Can quickly research, understand and use unfamiliar software technologies, tools and languages."));
            traits.Add(new Trait("Pragmatic", "Able to make a value judgement about what is really important, values practical outcomes and getting the job done, avoids gold plating."));
            traits.Add(new Trait("Honest", "Can admit mistakes, unafraid to admit they don't know something."));
            traits.Add(new Trait("Inquisitive Learner", "Actively self educates, reads and researches, willing to learn from others, always believes there is always much more to learn."));
            traits.Add(new Trait("Problem Solver", "Knows how to attack a problem and has the tenacity to solve even very hard problems, uses appropriate debugging tools."));
            traits.Add(new Trait("Researcher", "Good at ferreting out information: digging through documentation, searching the web, reading reference guides, release notes, discussion forums, mailing lists.<br><br>Knows how to find answers."));
            model.Traits = traits;

            return PartialView(model);
        }
        #endregion

    }

    
}
