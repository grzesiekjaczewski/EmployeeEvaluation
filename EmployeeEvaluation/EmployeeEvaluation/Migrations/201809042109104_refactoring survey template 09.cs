namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate09 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SurveyPartTemplates", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyPartTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
