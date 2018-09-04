namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id", "dbo.SurveyPartTemplates");
            DropIndex("dbo.SurveyQuestionTemplates", new[] { "SurveyPartTemplateExtended_Id" });
            DropColumn("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id", c => c.Int());
            CreateIndex("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id");
            AddForeignKey("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id", "dbo.SurveyPartTemplates", "Id");
        }
    }
}
