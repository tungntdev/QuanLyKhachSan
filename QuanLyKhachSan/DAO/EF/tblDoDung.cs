namespace DAO.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDoDung")]
    public partial class tblDoDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDoDung { get; set; }

        [StringLength(50)]
        public string TenDoDung { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGia { get; set; }
    }
}
