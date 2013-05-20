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
            var secondarySkills = new List<Skill>();

            // primary skills
            primarySkills.Add(new Skill(80, "C#",
                @"Understanding: Very Strong<br>" + 
                "Experience: 6 Years<br><br>" +
                "My primary language for both software and web development. Technologies include MSSQL, MySQL, MVC, EF, XNA, Mono and more."
            ));
            primarySkills.Add(new Skill(70, "ASP.NET",
                "Understanding: Strong<br>" +
                "Experience: 5 Years<br><br>" +
                "My go to framework for web applications. I have experience with asp.net-webforms but focus more on asp.net-mvc these days."
            ));
            primarySkills.Add(new Skill(60, "MVC",
                "Understanding: Solid<br>" +
                "Experience: 1 Year<br><br>" +
                "I've been focusing on learning MVC this past year, primarily asp.net-mvc. I have a solid understanding of how it works."
            ));

            // secondary skills
            secondarySkills.Add(new Skill(80, "HTML",
                "Understanding: Very Strong<br>" +
                "Experience: 8 Years<br><br>" +
                "I have been writing HTML for almost as long as I've been programming. I can hand code designs, but also have experience of boilerplate frameworks like Bootstrap and Foundation."
            ));
            secondarySkills.Add(new Skill(70, "CSS",
                "Understanding: Strong<br>" +
                "Experience: 7 Years<br><br>" +
                "CSS has been helping me make my websites look good for the last seven years or so. I like to think I am well acquainted with it's many features and intricacies."
            ));
            secondarySkills.Add(new Skill(50, "JS",
                "Understanding: Good<br>" +
                "Experience: 2+ Years<br><br>" +
                "My use of JavaScript has been sporadic. I don't focus on it primarily but have had success applying both jQuery and classic javascript to websites and canvas apps."
            ));
            secondarySkills.Add(new Skill(60, "PHP",
                "Understanding: Solid<br>" +
                "Experience: 5 Years<br><br>" +
                "My first web development language. I have a solid understanding of it, but focus more on .NET these days."
            ));
            secondarySkills.Add(new Skill(60, "SQL",
                "Understanding: Solid<br>" +
                "Experience: 8 Years<br><br>" +
                "I have a solid understanding of SQL and experience using all sorts of it from MySQL and MSSQL to Transact-SQL and Stored Procedures."
            ));

            model.PrimarySkills = primarySkills;
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