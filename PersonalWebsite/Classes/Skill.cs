namespace PersonalWebsite.Classes
{
    public class Skill
    {
        public string Title { get; set; }
        public int Value { get; set; }

        public Skill(string title, int value)
        {
            Title = title;
            Value = value;
        }
    }
}