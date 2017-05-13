namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUploadAndFileInfoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileInfoes",
                c => new
                    {
                        FileInfoId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        UploadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileInfoId)
                .ForeignKey("dbo.Uploads", t => t.UploadId, cascadeDelete: true)
                .Index(t => t.UploadId);
            
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        UploadId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(),
                        UploadPath = c.String(nullable: false, maxLength: 8000, unicode: false),
                        PublishDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UploadId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileInfoes", "UploadId", "dbo.Uploads");
            DropForeignKey("dbo.Uploads", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Uploads", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Uploads", new[] { "SubCategoryId" });
            DropIndex("dbo.Uploads", new[] { "CategoryId" });
            DropIndex("dbo.FileInfoes", new[] { "UploadId" });
            DropTable("dbo.Uploads");
            DropTable("dbo.FileInfoes");
        }
    }
}
