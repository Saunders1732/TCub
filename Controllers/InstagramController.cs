using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TCub.Controllers
{
    [Route("api/[controller]")]
    public class InstagramController : Controller
    {
        // GET: api/Instagram
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "inst value1", "inst value2" };
        }
 
       // GET api/Instagram/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string returnvalue = string.Empty;
            Models.InstagramModel thisModel = new Models.InstagramModel();
            if (id.ToLower() == "register")
            {
                returnvalue = thisModel.register(Context);
            }
            else if(id.ToLower() == "registerCallBack")
            {
                thisModel.registerCallback(Context);
            }
            else
            {
                returnvalue = "unhandled route";
            }
            return returnvalue;
        }
       

        // POST api/Instagram
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Instagram/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Instagram/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            

        }
    }
}
