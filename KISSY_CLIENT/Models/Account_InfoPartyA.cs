namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_InfoPartyA
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string CompanyName { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string NamePrefix { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(20)]
        public string IdCardNumber { get; set; }

        [StringLength(10)]
        public string IdCardIssued { get; set; }

        public DateTime? IdCardDate { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(20)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string Bank { get; set; }

        [StringLength(50)]
        public string TaxCode { get; set; }

        [StringLength(10)]
        public string DistrictId { get; set; }

        [StringLength(10)]
        public string ProvinceId { get; set; }

        [StringLength(10)]
        public string WardId { get; set; }
    }
}
