namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public System_User()
        {
            webpages_Roles = new HashSet<webpages_Roles>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string UserName { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string UserCode { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool? Sex { get; set; }

        public int? UserTypeId { get; set; }

        public int? LoginFailedCount { get; set; }

        public int? LoginFailedCount2 { get; set; }

        public int? LoginFailedCount3 { get; set; }

        public DateTime? LastChangedPassword { get; set; }

        public int? BranchId { get; set; }

        [StringLength(50)]
        public string ProfileImage { get; set; }

        public int? ParentId { get; set; }

        [StringLength(500)]
        public string DrugStore { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
