namespace FashionShop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int ID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage ="Nhập User name")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Nhập password")]
        public string Password { get; set; }

        public int? Type { get; set; }
    }
}
