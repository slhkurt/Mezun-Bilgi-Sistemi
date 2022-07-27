namespace Arel_MezunBilgiSistemi.Models
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
            Makalelers = new HashSet<Makaleler>();
            Yorumlars = new HashSet<Yorumlar>();
        }

        [Key]
        public int UyeId { get; set; }

        [StringLength(50)]
        public string AdiSoyadi { get; set; }

        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [StringLength(250)]
        public string Sifre { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Foto { get; set; }

        public DateTime? KayitTarihi { get; set; }

        public int? YetkiID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makaleler> Makalelers { get; set; }

        public virtual Yetkiler Yetkiler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
    }
}
