﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using MvcApplication16.Models;
using WebMatrix.WebData;

namespace MvcApplication16
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {

            AppDomain.CurrentDomain.SetData("DataTirectory",System.IO.Directory.GetCurrentDirectory()+"App_Data");

            //var q = System.IO.Directory.GetCurrentDirectory() + "App_Data";

            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: false);
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // следует обновить сайт. Дополнительные сведения: http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
