using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;


namespace TCub.Models
{
    /*
    https://instagram.com/developer/realtime/
    Create a Subscription  -->Pubsubhubub challenge flow.
    
    http://your-callback.com/url/?hub.mode=subscribe&hub.challenge={$guid}&hub.verify_token={$myVerifyToken}

    
    
    
    */
    public class InstagramModel: ThirdPartyModelBase
    {
        public string Id { get; set; }
        
        public override string register(HttpContext context)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    var json = httpClient.GetStringAsync(url).Result;
            //}
            return base.register(context);
        }

        public override void registerCallback(HttpContext context)
        {
            base.registerCallback(context);
        }
    
    }
}