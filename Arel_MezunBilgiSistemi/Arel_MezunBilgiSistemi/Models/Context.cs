namespace Arel_MezunBilgiSistemi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Etiketler> Etiketlers { get; set; }
        public virtual DbSet<Kategoriler> Kategorilers { get; set; }
        public virtual DbSet<Makaleler> Makalelers { get; set; }
        public virtual DbSet<Uyeler> Uyelers { get; set; }
        public virtual DbSet<Yetkiler> Yetkilers { get; set; }
        public virtual DbSet<Yorumlar> Yorumlars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etiketler>()
                .HasMany(e => e.Makalelers)
                .WithMany(e => e.Etiketlers)
                .Map(m => m.ToTable("MakaleEtiket").MapLeftKey("EtiketID").MapRightKey("MakaleID"));
        }
    }
}
