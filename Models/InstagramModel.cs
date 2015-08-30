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
<<<<<<< HEAD
         
        public override String register(HttpContext context)
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
=======
        [Required]
        public string register { get; set; }   
        
        /*
            curl -F 'client_id=CLIENT-ID' \
            -F 'client_secret=CLIENT-SECRET' \
            -F 'object=user' \
            -F 'aspect=media' \.
            -F 'verify_token=myVerifyToken' \
            -F 'callback_url=http://YOUR-CALLBACK/URL' \
            https://api.instagram.com/v1/subscriptions/ 
        */
        public void InitiateSubscription()
        {
            //var request = (HttpWebRequest)WebRequest.Create(TCub.Configuration());
           // request.Method = "POST";
            
               
        
        }    
>>>>>>> 7eee8c7b297ac1663ad00188b92f43bf103a23c3
    }
}