using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_API.Models.Account
{
    public class RegisterModel
    {
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

    public class ChangePasswordModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }

    public class facebookAppModel
    {
        public string app_name { get; set; }
        public string facebook_app_id { get; set; }
        public string AppSecret { get; set; }        
        public string fb_login_protocol_scheme { get; set; }
    }


    public class GoogleAppModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }


    public class CustomerInfo
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}