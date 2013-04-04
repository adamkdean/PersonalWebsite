namespace PersonalWebsite.Classes
{
    public class Skill
    {
        public int Value { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        

        public Skill(int value, string title, string description)
        {
            Value = value;
            Title = title;
            Description = description;            
        }

        /*private string GetTooltip()
        {
            if (Value > 90) return "Expert";
            else if (Value > 80) return "Superb";
            else if (Value > 70) return "Very Strong";
            else if (Value > 60) return "Strong";
            else if (Value > 50) return "Solid";
            else if (Value > 40) return "Mediocre";
            else if (Value > 30) return "Novice";
            else if (Value > 20) return "Weak";
            else if (Value > 10) return "Inadequate";
            else return "Terrible";
        }*/
    }
}