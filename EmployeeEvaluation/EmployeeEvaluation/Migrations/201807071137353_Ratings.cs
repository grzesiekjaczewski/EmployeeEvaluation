namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ratings");
        }
    }
}
