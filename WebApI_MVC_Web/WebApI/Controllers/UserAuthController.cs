using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace WebApI.Controllers
{
    public class UserAuthController : ApiController
    {
        [Authorize(Roles= "Admin: Read")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Authorize(Roles = "Manager")]
        public IHttpActionResult Post()
        {
            return Ok();
        }
    }
}
