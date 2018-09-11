namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingsandSurveySatatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveySatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Survey_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.Survey_Id)
                .Index(t => t.Survey_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveySatus", "Survey_Id", "dbo.Surveys");
            DropIndex("dbo.SurveySatus", new[] { "Survey_Id" });
            DropTable("dbo.SurveySatus");
        }
    }
}
