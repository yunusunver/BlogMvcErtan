namespace BlogMvcErtan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yetkilendirme")]
    public partial class Yetkilendirme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yetkilendirme()
        {
            Uyelers = new HashSet<Uyeler>();
        }

        [Key]
        public int YetkiId { get; set; }

        [StringLength(50)]
        public string Yetkisi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Uyeler> Uyelers { get; set; }
    }
}
