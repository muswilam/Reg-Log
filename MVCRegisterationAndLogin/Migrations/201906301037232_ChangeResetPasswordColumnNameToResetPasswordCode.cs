namespace MVCRegisterationAndLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeResetPasswordColumnNameToResetPasswordCode : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Users", "ResetPassword", "ResetPasswordCode");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Users", "ResetPasswordCode", "ResetPassword");
        }
    }
}
