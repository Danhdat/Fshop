namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuCategory")]
    public partial class MenuCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuCategory()
        {
            Product = new HashSet<Product>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string MenuCateName { get; set; }

        public string Link { get; set; }

        [StringLength(50)]
        public string Meta { get; set; }

        public bool? Hide { get; set; }

        public int? Order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Datebegin { get; set; }

        [StringLength(50)]
        public string MenuCateImg { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
