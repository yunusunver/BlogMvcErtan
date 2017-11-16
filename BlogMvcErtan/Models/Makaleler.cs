namespace BlogMvcErtan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Makaleler")]
    public partial class Makaleler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makaleler()
        {
            Yorumlars = new HashSet<Yorumlar>();
        }

        [Key]
        public int MakaleID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Makale Başlığı Giriniz")]
        public string MakaleBasligi { get; set; }
        [Required(ErrorMessage ="Makale içeriğini giriniz")]
        public string Makaleİcerik { get; set; }

        [StringLength(250)]
       
        public string MakaleFoto { get; set; }

        public DateTime? MakaleTarih { get; set; }

        public int? KategoriID { get; set; }

        public int? OkunmaSayisi { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
    }
}
