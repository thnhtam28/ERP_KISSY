using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class AccountModelClient
    {
    }
    public class LoginViewModel {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class RegisterModel2 {
        public string UserName { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }

    public class CustomerInfo2 {
        public string UserName { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

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

        [StringLength(50)]
        public string Image { get; set; }

        public int? UserId { get; set; }

        
    }
}