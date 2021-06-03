namespace HMS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AcomodationPackages", name: "AcomodationTypeID_ID", newName: "AcomodationType_ID");
            RenameIndex(table: "dbo.AcomodationPackages", name: "IX_AcomodationTypeID_ID", newName: "IX_AcomodationType_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AcomodationPackages", name: "IX_AcomodationType_ID", newName: "IX_AcomodationTypeID_ID");
            RenameColumn(table: "dbo.AcomodationPackages", name: "AcomodationType_ID", newName: "AcomodationTypeID_ID");
        }
    }
}
