namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class surveydisplay2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SurveyDisplays");
        }
        
        public override void Down()
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
    }
}
