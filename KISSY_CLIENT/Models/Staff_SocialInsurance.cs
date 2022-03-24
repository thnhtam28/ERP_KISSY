namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_SocialInsurance
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? StaffId { get; set; }

        [StringLength(150)]
        public string MedicalCode { get; set; }

        public DateTime? MedicalStartDate { get; set; }

        public DateTime? MedicalEndDate { get; set; }

        public string MedicalIssue { get; set; }

        public decimal? MedicalDefaultValue { get; set; }

        [StringLength(150)]
        public string SocietyCode { get; set; }

        public DateTime? SocietyStartDate { get; set; }

        public DateTime? SocietyEndDate { get; set; }

        public string SocietyIssue { get; set; }

        public decimal? SocietyDefaultValue { get; set; }

        public decimal? PC_CV { get; set; }

        public decimal? PC_TNVK { get; set; }

        public decimal? PC_TNN { get; set; }

        public decimal? PC_Khac { get; set; }

        public decimal? TienLuong { get; set; }

        public string Note { get; set; }

        [StringLength(150)]
        public string Status { get; set; }
    }
}
