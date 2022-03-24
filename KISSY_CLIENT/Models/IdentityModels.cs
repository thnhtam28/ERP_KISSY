using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ERP_API.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }

        public DateTime? Birthday { get; set; }
        public int? Sex { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(500)]
        public string F1 { get; set; }
        [StringLength(500)]
        public string F2 { get; set; }
        [StringLength(500)]
        public string F3 { get; set; }
        [StringLength(500)]
        public string F4 { get; set; }
        [StringLength(500)]
        public string F5 { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ErpDbContext", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ERP_API.Models.Page_Setup> Page_Setup { get; set; }

        public System.Data.Entity.DbSet<ERP_API.Models.Page_CategoryPost> Page_CategoryPost { get; set; }

        public System.Data.Entity.DbSet<ERP_API.Models.Page_New> Page_New { get; set; }

        public System.Data.Entity.DbSet<ERP_API.Models.Page_Slide> Page_Slide { get; set; }

        
    }
}