namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratings2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "Description");
        }
    }
}
