using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PunjabiHutProject.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model14")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Extra> Extras { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderExtraDetail> OrderExtraDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CATEGORY_FID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CUSROMER_FID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CUSTOMER_FID);

            modelBuilder.Entity<Extra>()
                .HasMany(e => e.OrderExtraDetails)
                .WithOptional(e => e.Extra)
                .HasForeignKey(e => e.EXTRA_FID);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.ORDER_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderExtraDetails)
                .WithOptional(e => e.Order)
                .HasForeignKey(e => e.ORDER_FID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Extras)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.PRODUCT_FID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.PRODUCT_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderExtraDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.PRODUCT_FID);
        }
    }
}
