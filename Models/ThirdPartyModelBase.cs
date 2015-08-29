using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace TCub.Models
{
    public class ThirdPartyModelBase
    {
        public virtual string register(HttpContext context)
        {
            return "";
        }


        public virtual void registerCallback(HttpContext context)
        {

            
        }

    }
}
