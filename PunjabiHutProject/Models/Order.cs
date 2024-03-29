namespace PunjabiHutProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderExtraDetails = new HashSet<OrderExtraDetail>();
        }

        [Key]
        public int ORDER_ID { get; set; }

        public DateTime? ORDER_DATE { get; set; }

        [StringLength(50)]
        public string ORDER_STATUS { get; set; }

        [StringLength(50)]
        public string ORDER_TYPE { get; set; }

        [Required]
        [StringLength(50)]
        public string ORDER_NAME { get; set; }

        [StringLength(50)]
        public string ORDER_EMAIL { get; set; }

        [StringLength(50)]
        public string ORDER_CONTACT { get; set; }

        [Required]
        [StringLength(100)]
        public string ORDER_ADDRESS { get; set; }


        [StringLength(50)]
        public string  STATUS { get; set; }

        public int? CUSTOMER_FID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderExtraDetail> OrderExtraDetails { get; set; }
    }
}
