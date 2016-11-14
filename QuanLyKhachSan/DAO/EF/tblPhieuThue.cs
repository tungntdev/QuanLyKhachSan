namespace DAO.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPhieuThue")]
    public partial class tblPhieuThue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPhieuThue()
        {
            tblChiTietPhieux = new HashSet<tblChiTietPhieu>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPhieuThue { get; set; }

        public int IDKhach { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianThue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianTra { get; set; }

        public int? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietPhieu> tblChiTietPhieux { get; set; }

        public virtual tblKhach tblKhach { get; set; }
    }
}
