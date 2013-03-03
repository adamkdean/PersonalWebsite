using PersonalWebsite.Classes;
using System.Collections.Generic;

namespace PersonalWebsite.Models.Profile
{
    public class SkillsViewModel
    {
        public List<Skill> PrimarySkills { get; set; }
        public List<Skill> SecondarySkills { get; set; }
    }
}