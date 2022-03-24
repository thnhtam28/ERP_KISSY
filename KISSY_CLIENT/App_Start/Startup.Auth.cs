using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ERP_API.Providers;
using ERP_API.Models;
using Microsoft.Owin.Security.Facebook;
using ERP_API.Filters;
using System.Configuration;
using System.Threading.Tasks;

namespace ERP_API
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            ReadDataSocial();
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie,TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

         

            //// Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
            //app.UseOAuthAuthorizationServer(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = _ClientId,
            //    ClientSecret = _ClientSecret
            //});
            //var facebookOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = _AppId,
            //    AppSecret = _AppSecret,
            //    BackchannelHttpHandler = new FacebookBackChannelHandler(),
            //    UserInformationEndpoint = "https://graph.facebook.com/v3.2/me?fields=id,email"
            //};
            //facebookOptions.Scope.Add("email");
            //app.UseFacebookAuthentication(facebookOptions);






            var facebookOptions = new FacebookAuthenticationOptions()
            {
                AppId = _AppId,
                AppSecret = _AppSecret,
            };

            // Set requested scope
            //facebookOptions.Scope.Add("email");
            //facebookOptions.Scope.Add("public_profile");

            //// Set requested fields
            //facebookOptions.Fields.Add("email");
            //facebookOptions.Fields.Add("first_name");
            //facebookOptions.Fields.Add("last_name");

            //facebookOptions.Provider = new FacebookAuthenticationProvider()
            //{
            //    OnAuthenticated = (context) =>
            //    {
            //        // Attach the access token if you need it later on for calls on behalf of the user
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));

            //        foreach (var claim in context.User)
            //        {
            //            //var claimType = string.Format("urn:facebook:{0}", claim.Key);
            //            var claimType = string.Format("{0}", claim.Key);
            //            string claimValue = claim.Value.ToString();

            //            if (!context.Identity.HasClaim(claimType, claimValue))
            //                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));
            //        }

            //        return Task.FromResult(0);
            //    }
            //};

            app.UseFacebookAuthentication(facebookOptions);





           
        }

        public static string _ClientId { get; set; }
        public static string _ClientSecret { get; set; }

        public static string _AppId { get; set; }
        public static string _AppSecret { get; set; }
        private void ReadDataSocial()
        {
            if (ConfigurationManager.AppSettings["AppId"] != null)
            {
                _AppId = ConfigurationManager.AppSettings["AppId"].ToString();
            }
            if (ConfigurationManager.AppSettings["AppSecret"] != null)
            {
                _AppSecret = ConfigurationManager.AppSettings["AppSecret"].ToString();
            }
            if (ConfigurationManager.AppSettings["ClientId"] != null)
            {
                _ClientId = ConfigurationManager.AppSettings["ClientId"].ToString();
            }
            if (ConfigurationManager.AppSettings["ClientSecret"] != null)
            {
                _ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
            }
        }
    }
}
