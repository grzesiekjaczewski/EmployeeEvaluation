namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishTemplate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Surveys", "SurveySatus_Id", "dbo.SurveySatus");
            DropIndex("dbo.Surveys", new[] { "SurveySatus_Id" });
            DropColumn("dbo.Surveys", "SurveySatus_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Surveys", "SurveySatus_Id", c => c.Int());
            CreateIndex("dbo.Surveys", "SurveySatus_Id");
            AddForeignKey("dbo.Surveys", "SurveySatus_Id", "dbo.SurveySatus", "Id");
        }
    }
}
