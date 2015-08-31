using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace TCub.Models
{
    public class ThirdPartyModelBase
    {
        private string _uri;

        public virtual string Uri { get { return _uri; } }

        public virtual Dictionary<string, string> RegisterParams { get; }

        public virtual async void register(HttpContext context)
        {
           
        }


        public virtual void registerCallback(HttpContext context)
        {
                        
        }

    }
}
