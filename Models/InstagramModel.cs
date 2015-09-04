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
            base._registerParams = RegisterParams;
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
                   // Send message to workflow         
                }
                else 
                {   //This is a Register Callback
                    if (!registerCallbackHandler(out ResponseValue, context))
                    { 
                        Error = ResponseValue;
                        ///TODO log Error
                    }  
                }
            }
            
            return ResponseValue;            
        }
        
        private bool registerCallbackHandler(out string ResponseValue, HttpContext context)
        {
            ResponseValue = string.Empty;
            bool returnvalue = false;
            
            PubSubHubBubLite pubsub = new PubSubHubBubLite(context, this._registerParams);
            if( pubsub.IsValid())
            {               
                ResponseValue = pubsub.Challenge;
                returnvalue = true;
            }
            else
            {
                ResponseValue = String.Join("," , pubsub.Error);
            }
            
            return returnvalue;
        }
        
        public class PubSubHubBubLite
        {
            private string _qstr;
            private string _mode;
            private string _verifyToken;
            private string _challenge;
           
            private Dictionary<string,string> Params;
            
            private List<string> _Error;
            
            private HttpContext _context;
            
            public string QueryString { get{return _qstr;}}
            public string Mode { get{return _mode;}}
            
            public string VerifyToken { get{return _verifyToken;}}
            public string Challenge { get{return _challenge;}}
            
            public List<string>Error {get {return _Error;}}
            
            public PubSubHubBubLite(HttpContext context, Dictionary<string,string> instanceParams){
                _qstr = context.Request.Query.ToString();         
                Params =  instanceParams;      
                _Error = new List<string>();    
                _context = context;                      
            }
            
            public bool IsValid()
            {   
                bool ResponseValue = false;                
                var QueryParam = _context.Request.Query;
                this._mode = QueryParam[InstagramModelStrings.HUB_MODE];
                this._verifyToken = QueryParam[InstagramModelStrings.HUB_VERIFY_TOKEN];
                this._challenge = QueryParam[InstagramModelStrings.HUB_CHALLENGE];            
            
                if (String.IsNullOrEmpty(_mode)) { _Error.Add(InstagramModelStrings.Error_Mode_Null); }
                if (_mode != InstagramModelStrings.HUB_MODE_EXPECTED){ _Error.Add(InstagramModelStrings.Error_Mode_INVALID);  }
                if (String.IsNullOrEmpty(_verifyToken)){_Error.Add(InstagramModelStrings.Error_TOKEN_NULL);}
                if (_verifyToken != InstagramModelStrings.VERIFY_TOKEN_VALUE){ _Error.Add(InstagramModelStrings.Error_TOKEN_INVALID);  }       
                if (String.IsNullOrEmpty(_challenge)){ _Error.Add(InstagramModelStrings.Error_Challenge_NULL); }
                
                Guid newGuid;
                if (Guid.TryParse(_challenge, out newGuid))
                {
                    ResponseValue = true;
                }            
                else 
                {
                    _Error.Add(InstagramModelStrings.Error_Challenge_INVALID);
                }
                
                return ResponseValue;
            }
            
        }
    }
}