using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BS.API.Infrastructure
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class BaseApiController : ControllerBase
    {
     
        public BaseApiController()
        {
           
        }
       
    }
}
