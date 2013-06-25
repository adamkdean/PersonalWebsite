// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace PersonalWebsite.Controllers
{
    public partial class ProfileController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProfileController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProfileController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProfileController Actions { get { return MVC.Profile; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Profile";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Profile";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string AboutMe = "AboutMe";
            public readonly string PieCharts = "PieCharts";
            public readonly string CoderbitsFooter = "CoderbitsFooter";
            public readonly string Skills = "Skills";
            public readonly string Traits = "Traits";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string AboutMe = "AboutMe";
            public const string PieCharts = "PieCharts";
            public const string CoderbitsFooter = "CoderbitsFooter";
            public const string Skills = "Skills";
            public const string Traits = "Traits";
        }


        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string AboutMe = "AboutMe";
                public readonly string CoderbitsFooter = "CoderbitsFooter";
                public readonly string Index = "Index";
                public readonly string PieCharts = "PieCharts";
                public readonly string Skills = "Skills";
                public readonly string Traits = "Traits";
            }
            public readonly string AboutMe = "~/Views/Profile/AboutMe.cshtml";
            public readonly string CoderbitsFooter = "~/Views/Profile/CoderbitsFooter.cshtml";
            public readonly string Index = "~/Views/Profile/Index.cshtml";
            public readonly string PieCharts = "~/Views/Profile/PieCharts.cshtml";
            public readonly string Skills = "~/Views/Profile/Skills.cshtml";
            public readonly string Traits = "~/Views/Profile/Traits.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProfileController : PersonalWebsite.Controllers.ProfileController
    {
        public T4MVC_ProfileController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void AboutMeOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult AboutMe()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.AboutMe);
            AboutMeOverride(callInfo);
            return callInfo;
        }

        partial void PieChartsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult PieCharts()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.PieCharts);
            PieChartsOverride(callInfo);
            return callInfo;
        }

        partial void CoderbitsFooterOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult CoderbitsFooter()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.CoderbitsFooter);
            CoderbitsFooterOverride(callInfo);
            return callInfo;
        }

        partial void SkillsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult Skills()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Skills);
            SkillsOverride(callInfo);
            return callInfo;
        }

        partial void TraitsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult Traits()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Traits);
            TraitsOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
