namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_WelfarePrograms
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public DateTime? ProvideStartDate { get; set; }

        public DateTime? ProvideEndDate { get; set; }

        public int? Quantity { get; set; }

        public int? TotalEstimatedCost { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(250)]
        public string Purpose { get; set; }

        [StringLength(50)]
        public string Formality { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public DateTime? RegistrationStartDate { get; set; }

        public DateTime? RegistrationEndDate { get; set; }

        public DateTime? ImplementationStartDate { get; set; }

        public DateTime? ImplementationEndDate { get; set; }

        public int? MoneyStaff { get; set; }

        public int? MoneyCompany { get; set; }

        public int? TotalStaffCompany { get; set; }

        public int? TotalMoneyStaff { get; set; }

        public int? TotalMoneyCompany { get; set; }

        public int? TotalStaffCompanyAll { get; set; }

        public int? TotalActualCosts { get; set; }

        [StringLength(50)]
        public string ApplicationObject { get; set; }

        [StringLength(150)]
        public string Code { get; set; }
    }
}
