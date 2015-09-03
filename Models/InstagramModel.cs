using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TCub.Models
{
    /*
    Based on Instagram API documentation
    https://instagram.com/developer/realtime/
    Last accessed 8/30/2015 

    Create a Subscription

    To create a subscription, you make a POST request to the subscriptions endpoint.

    curl -F 'client_id=CLIENT-ID' \
         -F 'client_secret=CLIENT-SECRET' \
         -F 'object=user' \
         -F 'aspect=media' \
         -F 'verify_token=myVerifyToken' \
         -F 'callback_url=http://YOUR-CALLBACK/URL' \
         https://api.instagram.com/v1/subscriptions/
   */
   
    public class InstagramModel: ThirdPartyModelBase
    {
       private Dictionary<string, string> _registerValues = null;
        public override string Uri{get {return base.Uri;}}
        
        public string Id { get; set; }
        
        public InstagramModel(){
            base._uri = InstagramModelStrings.BASE_URI_VALUE;
        }
        
        public override Dictionary<string, string> RegisterParams
        {
            get
            {
                if(_registerValues == null)
                {
                    _registerValues.Add(InstagramModelStrings.CLIENT_ID,        InstagramModelStrings.CLIENT_ID_VALUE);
                    _registerValues.Add(InstagramModelStrings.CLIENT_SECRET,    InstagramModelStrings.CLIENT_SECRET_VALUE);
                    _registerValues.Add(InstagramModelStrings.OBJECT,           InstagramModelStrings.OBJECT_VALUE);
                    _registerValues.Add(InstagramModelStrings.ASPECT,           InstagramModelStrings.ASPECT_VALUE);
                    _registerValues.Add(InstagramModelStrings.VERIFY_TOKEN,     InstagramModelStrings.VERIFY_TOKEN_VALUE);
                    _registerValues.Add(InstagramModelStrings.CALLBACK_URL,     InstagramModelStrings.CALLBACK_URL_VALUE);
                }

                return _registerValues;
            }
        }

        public override async Task<string> register(HttpContext context)
        {
              string returnvalue = string.Empty;
              returnvalue = await base.register(context);
              
              return returnvalue;
        }

        public override string Callback(HttpContext context)
        {
            //http://your-callback.com/url/?hub.mode=subscribe&hub.challenge=15f7d1a91c1f40f8a748fd134752feb3&hub.verify_token=myVerifyToken 
            string Error = InstagramModelStrings.Error_CALLBACK_INVALID;
            string ResponseValue = string.Empty;                   

            if (context.Request.Path == InstagramModelStrings.CALLBACK_URL)
            { 
                var QueryParam = context.Request.Query;
                var HUB_MODE_VALUE = QueryParam[InstagramModelStrings.HUB_MODE];
               
                if(String.IsNullOrEmpty(HUB_MODE_VALUE))
                {  //This is a standard callback from an event notification
                   ResponseValue = base.Callback(context);   
                   
                   ///TODO process instagram response JSON               
                }
                else 
                {   //This is a Register Callback
                    if (!registerCallbackHandler(out ResponseValue, context))
                    { 
                        Error = ResponseValue;
                        //log Error
                    }  
                }
            }
            
            return ResponseValue;            
        }
        
        private bool registerCallbackHandler(out string ResponseValue, HttpContext context)
        {
            bool IsValidChallenge = false;
            var QueryParam = context.Request.Query;
            var HUB_MODE_VALUE = QueryParam[InstagramModelStrings.HUB_MODE];
            var HUB_CHALLENGE_VALUE = QueryParam[InstagramModelStrings.HUB_CHALLENGE];
            var HUB_VERIFY_TOKEN_VALUE = QueryParam[InstagramModelStrings.HUB_VERIFY_TOKEN];
            string Error;
            ResponseValue = string.Empty;
                
            Error = InstagramModelStrings.Error_Mode_Null;
            if (!String.IsNullOrEmpty(HUB_MODE_VALUE))
            {
                Error = InstagramModelStrings.Error_Mode_INVALID; 
                if (HUB_MODE_VALUE == InstagramModelStrings.HUB_MODE_EXPECTED)
                {
                    Error = InstagramModelStrings.Error_TOKEN_NULL;
                    if (!String.IsNullOrEmpty(HUB_VERIFY_TOKEN_VALUE))
                    {
                        Error = InstagramModelStrings.Error_TOKEN_INVALID;
                        if (HUB_VERIFY_TOKEN_VALUE == InstagramModelStrings.VERIFY_TOKEN_VALUE)
                        {
                            Error = InstagramModelStrings.Error_Challenge_NULL;
                            if (!String.IsNullOrEmpty(HUB_CHALLENGE_VALUE))
                            {
                                Error = InstagramModelStrings.Error_Challenge_INVALID;
                                Guid newGuid;
                                if(Guid.TryParse(HUB_CHALLENGE_VALUE, out newGuid))
                                {
                                    IsValidChallenge = true;
                                    ResponseValue = HUB_CHALLENGE_VALUE;
                                }
                            }
                        }
                    }
                }
            }
            
            if(! IsValidChallenge){ResponseValue = Error;}
            return IsValidChallenge;
        }

    }
}