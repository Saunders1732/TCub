using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace TCub.ThirdParty
{
    public class common
    {
        public enum HttpMethodType
        {
             GET
            ,DELETE
            ,POST    
            ,PUT
        }
        
        public common()
        {
            
        }
        
        public static string CreatePostStream(Dictionary<string, string> Pairs)
        {
            string returnvalue = string.Empty;
            /* using(var qstr = new System.IO.StringWriter())
            {
                qstr.write("?");
               foreach (DictionaryEntry item in Pairs)
                {
                    qstr.write(item.key);
                    qstr.write("=");
                    qstr.write(item.value);
                }               
            } 
            returnvalue =qstr.ToString()
            */
            
            return returnvalue;
        }
    }
}
