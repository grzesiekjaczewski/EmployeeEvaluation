namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class surveydisplay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyDisplays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SurveyDadline = c.DateTime(nullable: false),
                        Status = c.String(),
                        EmployeeCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyDisplays");
        }
    }
}
