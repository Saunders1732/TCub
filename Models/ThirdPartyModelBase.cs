using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Http;
using Microsoft.Net.Http;
using System.Net;
using System.Net.Http;
using System.IO;

namespace TCub.Models
{
    public class ThirdPartyModelBase
    {
        protected string _uri;

        public virtual string Uri { get { return _uri; } }

        public virtual Dictionary<string, string> RegisterParams { get; }

        public virtual async Task<string> register(HttpContext context)
        {
            string returnvalue = string.Empty;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var responseMsg = await httpClient.PostAsync(
                                                      this.Uri
                                                    , new FormUrlEncodedContent(RegisterParams));
                    
                    if(responseMsg.StatusCode == HttpStatusCode.OK)
                    {    
                        string jsonMessage;
                        using (Stream responseStream = await responseMsg.Content.ReadAsStreamAsync())
                        {
                            jsonMessage = new StreamReader(responseStream).ReadToEnd();
                        }
                        
                        returnvalue = jsonMessage;
                        //json response is received when successful
                    }
                    else
                    {
                        //log error
                    }
                }
                catch (OperationCanceledException)
                {
                    //log error
                }                
            }          
           return returnvalue;
        }


        public virtual string Callback(HttpContext context)
        {
            //parse the message content for JSON data
            return string.Empty;           
        }

    }
}
