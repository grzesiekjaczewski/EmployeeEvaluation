namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingsandSurveySatatus3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surveys", "SurveyStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Surveys", "SurveySatus_Id", c => c.Int());
            CreateIndex("dbo.Surveys", "SurveySatus_Id");
            AddForeignKey("dbo.Surveys", "SurveySatus_Id", "dbo.SurveySatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "SurveySatus_Id", "dbo.SurveySatus");
            DropIndex("dbo.Surveys", new[] { "SurveySatus_Id" });
            DropColumn("dbo.Surveys", "SurveySatus_Id");
            DropColumn("dbo.Surveys", "SurveyStatusId");
        }
    }
}
