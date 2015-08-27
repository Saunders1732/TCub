using System.ComponentModel.DataAnnotations;


namespace TCub.Models
{
    /*
    https://instagram.com/developer/realtime/
    Create a Subscription  -->Pubsubhubub challenge flow.
    
    http://your-callback.com/url/?hub.mode=subscribe&hub.challenge={$guid}&hub.verify_token={$myVerifyToken}

    
    
    
    */
    public class InstagramModel
    {
        public string Id { get; set; }
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
    }
}