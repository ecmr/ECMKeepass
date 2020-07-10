using System;
using System.Collections.Generic;
using System.Text;

namespace ECMKeepass.AppConstant
{
    public class Constants
    {
        public static string AppName = "ECMKeepass";

        //OAuth
        public static string IOSClientId = "141676125689-31c6m4b5lhe1sj5l01v1ob3b0cicrlts.apps.googleusercontent.com";
        public static string AndroidClientId = "141676125689-bbsjfbgpb0h8p59mk37b18ra5h8isrsq.apps.googleusercontent.com";

        //These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        //set these to reserved IOS/Android client ids, with :/oauthredirect appended
        public static string IOSRedirectUrl = "com.googleusercontent.apps.141676125689-31c6m4b5lhe1sj5l01v1ob3b0cicrlts:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.141676125689-bbsjfbgpb0h8p59mk37b18ra5h8isrsq:/oauth2redirect";
    }
}
