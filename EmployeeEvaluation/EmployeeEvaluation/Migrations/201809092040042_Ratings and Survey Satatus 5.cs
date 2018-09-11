namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingsandSurveySatatus5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveySatus", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveySatus", "Score");
        }
    }
}
