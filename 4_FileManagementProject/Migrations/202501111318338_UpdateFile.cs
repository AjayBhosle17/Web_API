namespace _4_FileManagementProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFile : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Files", newName: "FilesDatas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FilesDatas", newName: "Files");
        }
    }
}
