namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_CHOP_DEAL
    {
        [Key]
        public int CHOP_DEAL_ID { get; set; }

        public int Product_Id { get; set; }

        public int SOLUONG { get; set; }

        public decimal DON_GIA { get; set; }

        public decimal THANH_TIEN { get; set; }

        public int DISCOUNT { get; set; }

        public int DEALHOT_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string HOTEN { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        public string DIENTHOAI { get; set; }

        [Required]
        [StringLength(550)]
        public string DIACHI { get; set; }

        [StringLength(1550)]
        public string GHICHU { get; set; }

        public int IS_OK { get; set; }

        public int? Invoice_Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }
    }
}
