namespace PersonalWebsite.Classes
{
    public class Trait
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Trait(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}