namespace _4_FileManagementProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        FileType = c.String(),
                        UploadedBy = c.Int(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        EntryIP = c.String(),
                        UpdateIP = c.String(),
                        UploadedUser_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.Users", t => t.UploadedUser_UserID)
                .Index(t => t.UploadedUser_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "UploadedUser_UserID", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UploadedUser_UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Files");
        }
    }
}
