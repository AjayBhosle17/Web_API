﻿namespace _2_Method_Para_With_Crud_IN_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Category");
        }
    }
}