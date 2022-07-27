namespace Arel_MezunBilgiSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Etiketler")]
    public partial class Etiketler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etiketler()
        {
            Makalelers = new HashSet<Makaleler>();
        }

        [Key]
        public int EtiketId { get; set; }

        [StringLength(50)]
        public string EtiketAdi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
