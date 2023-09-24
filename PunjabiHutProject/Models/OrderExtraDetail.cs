namespace PunjabiHutProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderExtraDetail")]
    public partial class OrderExtraDetail
    {
        [Key]
        public int EXTRADETAIL_ID { get; set; }

        public int? ORDER_FID { get; set; }

        public int? EXTRA_FID { get; set; }

        public int? PRODUCT_FID { get; set; }

        public virtual Extra Extra { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
