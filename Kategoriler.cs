namespace BlogMvcErtan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kategoriler")]
    public partial class Kategoriler
    {
        [Key]
        public int KategoriId { get; set; }

        [StringLength(50)]
        public string KategoriAdi { get; set; }

        [StringLength(250)]
        public string KategoriTanim { get; set; }

        public string Resim { get; set; }
    }
}
