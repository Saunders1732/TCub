using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCub.Models
{
    public class InstagramModelStrings
    {
        internal const string CLIENT_ID = "Instagram.client_id";
        internal static string CLIENT_ID_VALUE = Startup.configuration[CLIENT_ID];

        internal const string CLIENT_SECRET = "Instagram.client_secret";
        internal static string CLIENT_SECRET_VALUE = Startup.configuration[CLIENT_SECRET];

        internal const string OBJECT = "Instagram.object";
        internal static string OBJECT_VALUE = Startup.configuration[OBJECT];

        internal const string ASPECT = "Instagram.aspect";
        internal static string ASPECT_VALUE = Startup.configuration[ASPECT];

        internal const string VERIFY_TOKEN = "Instagram.verify_token";
        internal static string VERIFY_TOKEN_VALUE = Startup.configuration[VERIFY_TOKEN];

        internal const string CALLBACK_URL = "Instagram.callback_url";
        internal static string CALLBACK_URL_VALUE = Startup.configuration[CALLBACK_URL];

        internal const string HUB_MODE = "hub.mode";
        internal static string HUB_MODE_EXPECTED = Startup.configuration[HUB_MODE];
        internal const string HUB_CHALLENGE = "hub.challenge";
        internal static string HUB_VERIFY_TOKEN = "verify_token";

        internal const string BASE_URI = "Instagram.base_uri";

        
        internal const string Error_CALLBACK_INVALID = "Invalid callback url";
        internal const string Error_Mode_Null = "Mode not provided";
        internal const string Error_Mode_INVALID = "Invalid Mode";
        internal const string Error_TOKEN_NULL = "Token not provided";
        internal const string Error_TOKEN_INVALID = "Invalid Token";
        internal const string Error_Challenge_NULL = "Challenge not provided";
        internal const string Error_Challenge_INVALID = "Invalid Challenge";
    }
}
