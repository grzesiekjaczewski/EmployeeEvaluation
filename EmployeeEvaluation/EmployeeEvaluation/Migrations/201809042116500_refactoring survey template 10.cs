namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate10 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", "dbo.SurveyTemplates");
            //DropIndex("dbo.SurveyPartTemplates", new[] { "SurveyTemplateExtended_Id" });
            //DropColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", c => c.Int());
            //CreateIndex("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id");
            //AddForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", "dbo.SurveyTemplates", "Id");
        }
    }
}
