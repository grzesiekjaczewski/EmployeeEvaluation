namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HireDate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.String());
        }
    }
}
