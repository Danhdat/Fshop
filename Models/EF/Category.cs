namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            MenuCategory = new HashSet<MenuCategory>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        public string CateDescription { get; set; }

        [StringLength(50)]
        public string CateImg { get; set; }

        [StringLength(50)]
        public string Meta { get; set; }

        public bool? Hide { get; set; }

        public int? Order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Datebegin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuCategory> MenuCategory { get; set; }
    }
}
