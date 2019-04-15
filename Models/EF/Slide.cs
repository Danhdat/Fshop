namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string SlideName { get; set; }

        [StringLength(50)]
        public string SlideImg { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        public string Meta { get; set; }

        public bool? Hide { get; set; }

        public int? Order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Datebegin { get; set; }
    }
}
