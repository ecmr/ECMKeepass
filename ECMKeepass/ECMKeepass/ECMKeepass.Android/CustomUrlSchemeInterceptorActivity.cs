using Android.App;
using Android.Content;
using Android.OS;
using ECMKeepass.AuthHelpers;
using System;

namespace ECMKeepass.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop )]
    [IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataSchemes = new[] { "com.googleusercontent.apps.141676125689-bbsjfbgpb0h8p59mk37b18ra5h8isrsq" },
    DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var uri = new Uri(Intent.Data.ToString());

            AuthenticationState.Authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}