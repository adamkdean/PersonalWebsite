namespace PersonalWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostId = c.Int(nullable: false, identity: true),
                        BlogTitle = c.String(),
                        BlogContent = c.String(),
                        Slug = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        Views = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogPostId);
            
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
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId)
                .Index(t => t.BlogPost_BlogPostId);
            
            CreateTable(
                "dbo.FailedAttempts",
                c => new
                    {
                        FailedAttemptId = c.Int(nullable: false, identity: true),
                        DateAttempted = c.DateTime(nullable: false),
                        UsernameGiven = c.String(),
                        IPAddress = c.String(),
                    })
                .PrimaryKey(t => t.FailedAttemptId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Hash = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.BlogPostTags",
                c => new
                    {
                        BlogPost_BlogPostId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogPost_BlogPostId, t.Tag_TagId })
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.BlogPost_BlogPostId)
                .Index(t => t.Tag_TagId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BlogPostTags", new[] { "Tag_TagId" });
            DropIndex("dbo.BlogPostTags", new[] { "BlogPost_BlogPostId" });
            DropIndex("dbo.Comments", new[] { "BlogPost_BlogPostId" });
            DropForeignKey("dbo.BlogPostTags", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.BlogPostTags", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.Comments", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropTable("dbo.BlogPostTags");
            DropTable("dbo.Users");
            DropTable("dbo.FailedAttempts");
            DropTable("dbo.Comments");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.Tags");
        }
    }
}
