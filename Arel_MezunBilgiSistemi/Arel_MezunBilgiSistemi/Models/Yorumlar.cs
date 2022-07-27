namespace Arel_MezunBilgiSistemi.Models
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

        public string Icerik { get; set; }

        public DateTime? Tarihi { get; set; }

        public int? MakaleID { get; set; }

        public int? UyeID { get; set; }

        public virtual Makaleler Makaleler { get; set; }

        public virtual Uyeler Uyeler { get; set; }
    }
}
