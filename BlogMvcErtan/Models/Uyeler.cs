namespace BlogMvcErtan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uyeler")]
    public partial class Uyeler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uyeler()
        {
            Yorumlars = new HashSet<Yorumlar>();
        }

        [Key]
        public int UyeId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Kullanıcı adı giriniz.")]
        public string KullanıcıAdı { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Şifre giriniz.")]
        public string Password { get; set; }

        [StringLength(50)]
        public string UyeAdSoyad { get; set; }

        [StringLength(250)]
        public string UyeFoto { get; set; }

        public int? YetkiId { get; set; }

        public DateTime? Tarih { get; set; }

        public virtual Yetkilendirme Yetkilendirme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
    }
}
