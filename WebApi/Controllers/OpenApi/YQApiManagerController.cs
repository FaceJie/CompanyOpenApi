using APIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WebApi.Controllers.OpenApi
{
    public class YQApiManagerController : ApiController
    {

        // GET api/values
        public void Set()
        {


            string str=YQOpenApiHelper.CreateToken("10245","GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk").msg;
        }

        public void Get()
        {

            string str = YQOpenApiHelper.CreateToken("10245", "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk").msg;

            string result = YQOpenApiHelper.ValidateToken(str).msg;

        }
    }
}
