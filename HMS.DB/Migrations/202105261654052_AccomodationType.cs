namespace HMS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccomodationType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AcomodationPackages", "AcomodationTypeID", "dbo.AcomodationTypes");
            DropIndex("dbo.AcomodationPackages", new[] { "AcomodationTypeID" });
            DropColumn("dbo.AcomodationPackages", "AcomodationTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AcomodationPackages", "AcomodationTypeID", c => c.Int());
            CreateIndex("dbo.AcomodationPackages", "AcomodationTypeID");
            AddForeignKey("dbo.AcomodationPackages", "AcomodationTypeID", "dbo.AcomodationTypes", "ID");
        }
    }
}
