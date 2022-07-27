namespace Arel_MezunBilgiSistemi.Models
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
            Etiketlers = new HashSet<Etiketler>();
        }

        [Key]
        public int MakaleId { get; set; }

        [StringLength(50)]
        public string Baslik { get; set; }

        public string Icerik { get; set; }

        public DateTime? YayinTarih { get; set; }

        public int? Okunma { get; set; }

        [StringLength(250)]
        public string Foto { get; set; }

        public int? KategoriID { get; set; }

        public int? UyeID { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }

        public virtual Uyeler Uyeler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiketler> Etiketlers { get; set; }
    }
}
