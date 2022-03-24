using ERP_API.Models.Account;
using ERP_API.Operation.API;
using ERP_API.Operation.Nomal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{
    public class API_LoginController : ApiController
    {
        UserOperation userOperation;
        ApiSocialOperation apiSocialOperation;
        public API_LoginController()
        {
            userOperation = new UserOperation();
            apiSocialOperation = new ApiSocialOperation();
        }

        [HttpGet]
        [Route("api/login/UrlRequestSocial/{id}")]
        public IHttpActionResult UrlRequestSocial(string id)
        {
            return Json(apiSocialOperation.GetProvide(id));
        }

        [HttpGet]
        [Route("api/login/{username}/{password}")]
        public IHttpActionResult Login(string username, string password)
        {
            CustomerInfo user = userOperation.Login(username, password);
            return Json(user);
        }

        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult Register(RegisterModel registerModel)
        {
            int result = userOperation.Register(registerModel);
            return Json(result);
        }

        [HttpPut]
        [Route("api/changepassword")]
        public IHttpActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            bool result = userOperation.ChangePassword(changePasswordModel);
            return Json(result);
        }
    }
}
