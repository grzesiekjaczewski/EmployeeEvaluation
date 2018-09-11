namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingsandSurveySatatus2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveySatus", "Survey_Id", "dbo.Surveys");
            DropIndex("dbo.SurveySatus", new[] { "Survey_Id" });
            DropColumn("dbo.SurveySatus", "Survey_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveySatus", "Survey_Id", c => c.Int());
            CreateIndex("dbo.SurveySatus", "Survey_Id");
            AddForeignKey("dbo.SurveySatus", "Survey_Id", "dbo.Surveys", "Id");
        }
    }
}
