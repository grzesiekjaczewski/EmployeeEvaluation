namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Survey3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SurveyPartTemplates", name: "SurveyTemplateExtemded_Id", newName: "SurveyTemplateExtended_Id");
            RenameIndex(table: "dbo.SurveyPartTemplates", name: "IX_SurveyTemplateExtemded_Id", newName: "IX_SurveyTemplateExtended_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SurveyPartTemplates", name: "IX_SurveyTemplateExtended_Id", newName: "IX_SurveyTemplateExtemded_Id");
            RenameColumn(table: "dbo.SurveyPartTemplates", name: "SurveyTemplateExtended_Id", newName: "SurveyTemplateExtemded_Id");
        }
    }
}
