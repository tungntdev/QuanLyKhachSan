namespace DAO.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDichVu")]
    public partial class tblDichVu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDichVu { get; set; }

        [StringLength(50)]
        public string TenDichVu { get; set; }

        public int? GiaThanh { get; set; }
    }
}
