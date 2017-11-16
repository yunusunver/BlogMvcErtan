namespace BlogMvcErtan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorumlar")]
    public partial class Yorumlar
    {
        [Key]
        public int YorumId { get; set; }
        [Required(ErrorMessage ="Yorum içeriðini giriniz")]
        public string YorumIcerik { get; set; }

        public DateTime? YorumTarihi { get; set; }

        public int? UyeId { get; set; }

        public int? MakaleId { get; set; }

        public virtual Makaleler Makaleler { get; set; }

        public virtual Uyeler Uyeler { get; set; }
    }
}
