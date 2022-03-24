namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_Customer
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        [StringLength(150)]
        public string CompanyName { get; set; }

        public int? BranchId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? Gender { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        public string WardId { get; set; }

        [StringLength(10)]
        public string DistrictId { get; set; }

        [StringLength(10)]
        public string CityId { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Point { get; set; }

        [StringLength(50)]
        public string CardCode { get; set; }

        [StringLength(50)]
        public string SearchFullName { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        [StringLength(20)]
        public string IdCardNumber { get; set; }

        public DateTime? IdCardDate { get; set; }

        [StringLength(10)]
        public string IdCardIssued { get; set; }

        [StringLength(150)]
        public string CompanyNameSearch { get; set; }

        public int? OldDB_ID { get; set; }

        [StringLength(50)]
        public string CustomerType { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string PositionCode { get; set; }

        [StringLength(50)]
        public string Pass_word { get; set; }

        [StringLength(15)]
        public string TaxCode { get; set; }

        [StringLength(50)]
        public string BankAccount { get; set; }

        [StringLength(200)]
        public string BankName { get; set; }

        [StringLength(350)]
        public string GroupPrice { get; set; }
    }
}
