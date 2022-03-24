namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_Staff
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? Gender { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string IdCardNumber { get; set; }

        public DateTime? IdCardDate { get; set; }

        [StringLength(10)]
        public string IdCardIssued { get; set; }

        [StringLength(50)]
        public string Ethnic { get; set; }

        [StringLength(50)]
        public string Religion { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Literacy { get; set; }

        [StringLength(50)]
        public string Technique { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        public int? BranchDepartmentId { get; set; }

        public int? PositionId { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [StringLength(10)]
        public string ProvinceId { get; set; }

        [StringLength(10)]
        public string DistrictId { get; set; }

        [StringLength(10)]
        public string WardId { get; set; }

        [StringLength(50)]
        public string ProfileImage { get; set; }

        [StringLength(10)]
        public string CountryId { get; set; }

        public int? UserId { get; set; }

        [StringLength(20)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Email2 { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? CheckInOut_UserId { get; set; }

        public int? Sale_BranchId { get; set; }

        public bool? IsWorking { get; set; }

        public string DrugStore { get; set; }

        public int? StaffParentId { get; set; }

        public decimal? CommissionPercent { get; set; }

        public decimal? MinimumRevenue { get; set; }
    }
}
