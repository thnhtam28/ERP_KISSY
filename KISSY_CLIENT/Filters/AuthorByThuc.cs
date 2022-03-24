using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERP_API.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorByThuc: AuthorizeAttribute
    {
       
    
        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (HttpContext.Current.Session["RoleUser"] == null)
            {
                filterContext.Result = new RedirectResult("/View_Login" + "?returnUrl=" + filterContext.HttpContext.Request.Url);
            }
        
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session["RoleUser"] == null)
                return false;
            else
                return true;
            
        }


    }
}