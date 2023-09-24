namespace PunjabiHutProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changing1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        ADMIN_ID = c.Int(nullable: false, identity: true),
                        ADMIN_NAME = c.String(nullable: false, maxLength: 50),
                        ADMIN_EMAIL = c.String(nullable: false, maxLength: 50),
                        ADMIN_PASSWARD = c.String(nullable: false, maxLength: 50),
                        ADMIN_CONTACT = c.String(maxLength: 50),
                        ADMIN_ADDRESS = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ADMIN_ID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CATEGORY_ID = c.Int(nullable: false, identity: true),
                        CATEGORY_NAME = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CATEGORY_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        PRODUCT_ID = c.Int(nullable: false, identity: true),
                        PRODUCT_NAME = c.String(maxLength: 50),
                        PRODUCT_DESCRIPTION = c.String(maxLength: 100),
                        PRODUCT_PURCHASEPRICE = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        PRODUCT_SALEPRICE = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        PRODUCT_PICTURE = c.String(maxLength: 200),
                        CATEGORY_FID = c.Int(),
                    })
                .PrimaryKey(t => t.PRODUCT_ID)
                .ForeignKey("dbo.Category", t => t.CATEGORY_FID)
                .Index(t => t.CATEGORY_FID);
            
            CreateTable(
                "dbo.Extra",
                c => new
                    {
                        EXTRA_ID = c.Int(nullable: false, identity: true),
                        EXTRA_NAME = c.String(maxLength: 50),
                        EXTRA_SALEPRICE = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        EXTRA_PURCHASEPRICE = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        PRODUCT_FID = c.Int(),
                    })
                .PrimaryKey(t => t.EXTRA_ID)
                .ForeignKey("dbo.Product", t => t.PRODUCT_FID)
                .Index(t => t.PRODUCT_FID);
            
            CreateTable(
                "dbo.OrderExtraDetail",
                c => new
                    {
                        EXTRADETAIL_ID = c.Int(nullable: false, identity: true),
                        ORDER_FID = c.Int(),
                        EXTRA_FID = c.Int(),
                        PRODUCT_FID = c.Int(),
                    })
                .PrimaryKey(t => t.EXTRADETAIL_ID)
                .ForeignKey("dbo.Orders", t => t.ORDER_FID)
                .ForeignKey("dbo.Extra", t => t.EXTRA_FID)
                .ForeignKey("dbo.Product", t => t.PRODUCT_FID)
                .Index(t => t.ORDER_FID)
                .Index(t => t.EXTRA_FID)
                .Index(t => t.PRODUCT_FID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ORDER_ID = c.Int(nullable: false, identity: true),
                        ORDER_DATE = c.DateTime(),
                        ORDER_STATUS = c.String(maxLength: 50),
                        ORDER_TYPE = c.String(maxLength: 50),
                        ORDER_NAME = c.String(nullable: false, maxLength: 50),
                        ORDER_EMAIL = c.String(maxLength: 50),
                        ORDER_CONTACT = c.String(maxLength: 50),
                        ORDER_ADDRESS = c.String(nullable: false, maxLength: 100),
                        STATUS = c.String(maxLength: 50),
                        CUSTOMER_FID = c.Int(),
                    })
                .PrimaryKey(t => t.ORDER_ID)
                .ForeignKey("dbo.Customer", t => t.CUSTOMER_FID)
                .Index(t => t.CUSTOMER_FID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CUSTOMER_ID = c.Int(nullable: false, identity: true),
                        CUSTOMER_NAME = c.String(maxLength: 50),
                        CUSTOMER_EMAIL = c.String(maxLength: 50),
                        CUSTOMER_ADDRESS = c.String(maxLength: 200),
                        CUSTOMER_CONTACT = c.String(maxLength: 50),
                        CUSTOMER_PASSWORD = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CUSTOMER_ID);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        FEEDBACK_ID = c.Int(nullable: false, identity: true),
                        FEEDBACK_DETAIL = c.String(maxLength: 50),
                        FEEDBACK_EMAIL = c.String(maxLength: 50),
                        FEEDBACK_CONTACT = c.String(maxLength: 50),
                        FEEDBACK_ADDRESS = c.String(maxLength: 50),
                        CUSROMER_FID = c.Int(),
                    })
                .PrimaryKey(t => t.FEEDBACK_ID)
                .ForeignKey("dbo.Customer", t => t.CUSROMER_FID)
                .Index(t => t.CUSROMER_FID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        ORDERDETAIL_ID = c.Int(nullable: false, identity: true),
                        ORDER_FID = c.Int(nullable: false),
                        PRODUCT_FID = c.Int(nullable: false),
                        SALE_PRICE = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        PURCHASE_PRICE = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        QUANTITY = c.Int(),
                    })
                .PrimaryKey(t => t.ORDERDETAIL_ID)
                .ForeignKey("dbo.Orders", t => t.ORDER_FID)
                .ForeignKey("dbo.Product", t => t.PRODUCT_FID)
                .Index(t => t.ORDER_FID)
                .Index(t => t.PRODUCT_FID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "CATEGORY_FID", "dbo.Category");
            DropForeignKey("dbo.OrderExtraDetail", "PRODUCT_FID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "PRODUCT_FID", "dbo.Product");
            DropForeignKey("dbo.Extra", "PRODUCT_FID", "dbo.Product");
            DropForeignKey("dbo.OrderExtraDetail", "EXTRA_FID", "dbo.Extra");
            DropForeignKey("dbo.OrderExtraDetail", "ORDER_FID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetail", "ORDER_FID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CUSTOMER_FID", "dbo.Customer");
            DropForeignKey("dbo.Feedback", "CUSROMER_FID", "dbo.Customer");
            DropIndex("dbo.OrderDetail", new[] { "PRODUCT_FID" });
            DropIndex("dbo.OrderDetail", new[] { "ORDER_FID" });
            DropIndex("dbo.Feedback", new[] { "CUSROMER_FID" });
            DropIndex("dbo.Orders", new[] { "CUSTOMER_FID" });
            DropIndex("dbo.OrderExtraDetail", new[] { "PRODUCT_FID" });
            DropIndex("dbo.OrderExtraDetail", new[] { "EXTRA_FID" });
            DropIndex("dbo.OrderExtraDetail", new[] { "ORDER_FID" });
            DropIndex("dbo.Extra", new[] { "PRODUCT_FID" });
            DropIndex("dbo.Product", new[] { "CATEGORY_FID" });
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Feedback");
            DropTable("dbo.Customer");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderExtraDetail");
            DropTable("dbo.Extra");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
            DropTable("dbo.Admin");
        }
    }
}
