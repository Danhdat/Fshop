namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int ID { get; set; }

        public string NewName { get; set; }

        [StringLength(50)]
        public string NewImg { get; set; }

        public string Desciption { get; set; }

        public string Link { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public string Meta { get; set; }

        public bool? Hide { get; set; }

        public int? Order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Datebegin { get; set; }
    }
}
