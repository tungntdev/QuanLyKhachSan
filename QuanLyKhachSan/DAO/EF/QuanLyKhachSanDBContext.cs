namespace DAO.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuanLyKhachSanDBContext : DbContext
    {
        public QuanLyKhachSanDBContext()
            : base("name=QuanLyKhachSanDBContext1")
        {
        }

        public virtual DbSet<tblChiTietPhieu> tblChiTietPhieux { get; set; }
        public virtual DbSet<tblDichVu> tblDichVus { get; set; }
        public virtual DbSet<tblDoDung> tblDoDungs { get; set; }
        public virtual DbSet<tblKhach> tblKhaches { get; set; }
        public virtual DbSet<tblPhieuThue> tblPhieuThues { get; set; }
        public virtual DbSet<tblPhong> tblPhongs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblKhach>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<tblKhach>()
                .HasMany(e => e.tblPhieuThues)
                .WithRequired(e => e.tblKhach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblPhieuThue>()
                .HasMany(e => e.tblChiTietPhieux)
                .WithRequired(e => e.tblPhieuThue)
                .WillCascadeOnDelete(false);
        }
    }
}
