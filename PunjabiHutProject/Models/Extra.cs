namespace PunjabiHutProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Extra")]
    public partial class Extra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Extra()
        {
            OrderExtraDetails = new HashSet<OrderExtraDetail>();
        }

        [Key]
        public int EXTRA_ID { get; set; }

        [StringLength(50)]
        public string EXTRA_NAME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EXTRA_SALEPRICE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EXTRA_PURCHASEPRICE { get; set; }

        public int? PRODUCT_FID { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderExtraDetail> OrderExtraDetails { get; set; }
    }
}
