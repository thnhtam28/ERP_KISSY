namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_FPMachine
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(20)]
        public string Ten_may { get; set; }

        [StringLength(50)]
        public string Loai_ket_noi { get; set; }

        public int? Ma_loai_ket_noi { get; set; }

        [StringLength(20)]
        public string ID_Ket_noi_COM { get; set; }

        [StringLength(20)]
        public string ID_Ket_noi_IP { get; set; }

        public int? Cong_COM { get; set; }

        [StringLength(15)]
        public string Dia_chi_IP { get; set; }

        [StringLength(6)]
        public string Toc_do_truyen { get; set; }

        public int? Port { get; set; }

        public int? Loaimay { get; set; }

        public int? Passwd { get; set; }

        [StringLength(50)]
        public string url { get; set; }

        public bool? useurl { get; set; }

        public int? AutoID { get; set; }

        [StringLength(50)]
        public string Ten_may_tinh { get; set; }

        [StringLength(50)]
        public string TeamviewerID { get; set; }

        [StringLength(50)]
        public string TeamviewerPassword { get; set; }

        [StringLength(150)]
        public string GetDataSchedule { get; set; }

        public int? BranchId { get; set; }

        [StringLength(300)]
        public string Note { get; set; }
    }
}
