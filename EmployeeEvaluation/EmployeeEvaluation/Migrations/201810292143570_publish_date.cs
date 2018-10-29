namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publish_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyTemplates", "PublishDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyTemplates", "PublishDate");
        }
    }
}
