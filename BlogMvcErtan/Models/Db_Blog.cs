namespace BlogMvcErtan.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Db_Blog : DbContext
    {
        public Db_Blog()
            : base("name=Db_Blog")
        {
        }

        public virtual DbSet<Kategoriler> Kategorilers { get; set; }
        public virtual DbSet<Makaleler> Makalelers { get; set; }
        public virtual DbSet<Uyeler> Uyelers { get; set; }
        public virtual DbSet<Yetkilendirme> Yetkilendirmes { get; set; }
        public virtual DbSet<Yorumlar> Yorumlars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
