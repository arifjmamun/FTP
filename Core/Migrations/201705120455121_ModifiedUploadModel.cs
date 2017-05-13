namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedUploadModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uploads", "DriveLetter", c => c.String(nullable: false, maxLength: 5, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uploads", "DriveLetter");
        }
    }
}
