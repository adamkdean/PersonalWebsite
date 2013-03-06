namespace PersonalWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBlogPostComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.Comments", new[] { "BlogPost_BlogPostId" });
            DropTable("dbo.Comments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentAuthor = c.String(),
                        CommentContent = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        IPAddress = c.String(),
                        BlogPost_BlogPostId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateIndex("dbo.Comments", "BlogPost_BlogPostId");
            AddForeignKey("dbo.Comments", "BlogPost_BlogPostId", "dbo.BlogPosts", "BlogPostId");
        }
    }
}
