namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public double? ProductPrice { get; set; }

        [StringLength(50)]
        public string ProductSize { get; set; }

        [StringLength(30)]
        public string ProductColor { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string ProductImg { get; set; }

        [StringLength(50)]
        public string Meta { get; set; }

        public bool? Hide { get; set; }

        public int? Order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Datebegin { get; set; }

        public string ProductDescription { get; set; }

        public int? MenuCateID { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
