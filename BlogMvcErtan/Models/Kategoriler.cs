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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategoriler()
        {
            Makalelers = new HashSet<Makaleler>();
        }

        [Key]
        public int KategoriId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Kategori ismi giriniz.")]
        public string KategoriAdi { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Kategori tanýmý giriniz.")]
        public string KategoriTanim { get; set; }
     
        public string Resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
