using EasyAuth.Storage;
using System.Data.Entity;

namespace PersonalWebsite.Models
{
    public class WebsiteContext : UserStoreContext
    {
        public WebsiteContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<CodePost> CodePosts { get; set; }        
        public DbSet<FailedAttempt> FailedAttempts { get; set; }
    }
}