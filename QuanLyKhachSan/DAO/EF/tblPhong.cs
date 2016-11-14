namespace DAO.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPhong")]
    public partial class tblPhong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPhong { get; set; }

        public int? SucChua { get; set; }

        public int GiaPhong { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }
    }
}
