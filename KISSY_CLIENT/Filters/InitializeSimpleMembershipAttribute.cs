using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace ERP_API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        //UserRepository userRepository = new UserRepository(new ErpDbContext());

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //    string FirstCurrentName = string.Empty;
        //    var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
        //    FirstCurrentName = user != null ? user.FirstName : string.Empty;
        //    filterContext.Controller.ViewBag.FirstCurrentName = FirstCurrentName;
        //}

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
               // Database.SetInitializer<UsersContext>(null);

                try
                {
                    WebSecurity.InitializeDatabaseConnection("ErpDbContext", "System_User", "Id", "UserName", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}