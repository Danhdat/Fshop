namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string MenuName { get; set; }

        public string Link { get; set; }

        [StringLength(50)]
        public string Meta { get; set; }

        public bool? Hide { get; set; }

        public int? Order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Datebegin { get; set; }
    }
}
