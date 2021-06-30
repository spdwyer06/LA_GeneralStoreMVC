namespace GeneralStore_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionItemRefactorTransaction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "ProductId", "dbo.Products");
            DropIndex("dbo.Transactions", new[] { "ProductId" });
            RenameColumn(table: "dbo.Transactions", name: "ProductId", newName: "Product_Id");
            CreateTable(
                "dbo.TransactionItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId)
                .Index(t => t.ProductId);
            
            AlterColumn("dbo.Transactions", "Product_Id", c => c.Int());
            CreateIndex("dbo.Transactions", "Product_Id");
            AddForeignKey("dbo.Transactions", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.Transactions", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.Transactions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.TransactionItems", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.TransactionItems", "ProductId", "dbo.Products");
            DropIndex("dbo.TransactionItems", new[] { "ProductId" });
            DropIndex("dbo.TransactionItems", new[] { "TransactionId" });
            DropIndex("dbo.Transactions", new[] { "Product_Id" });
            AlterColumn("dbo.Transactions", "Product_Id", c => c.Int(nullable: false));
            DropTable("dbo.TransactionItems");
            RenameColumn(table: "dbo.Transactions", name: "Product_Id", newName: "ProductId");
            CreateIndex("dbo.Transactions", "ProductId");
            AddForeignKey("dbo.Transactions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
