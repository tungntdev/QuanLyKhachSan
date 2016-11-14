namespace DAO.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblKhach")]
    public partial class tblKhach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblKhach()
        {
            tblPhieuThues = new HashSet<tblPhieuThue>();
        }

        [Key]
        public int IDKhach { get; set; }

        [StringLength(50)]
        public string TenKhach { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        public int? SoNguoi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPhieuThue> tblPhieuThues { get; set; }
    }
}
