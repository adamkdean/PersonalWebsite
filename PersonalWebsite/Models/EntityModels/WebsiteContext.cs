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

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}