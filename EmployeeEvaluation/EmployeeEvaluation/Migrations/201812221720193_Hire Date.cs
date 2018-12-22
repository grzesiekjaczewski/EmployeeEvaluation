namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HireDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "HireDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "HireDate");
        }
    }
}
