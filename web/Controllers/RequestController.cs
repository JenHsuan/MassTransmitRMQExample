using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web.Contracts;

namespace web.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestClient<CheckOrderStatus> _checkStatusClient;
        public RequestController(IRequestClient<CheckOrderStatus> checkStatusClient)
        {
            _checkStatusClient = checkStatusClient;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _checkStatusClient.GetResponse<OrderStatusResult>(new { OrderId = id });
            return Ok(response.Message);
        }
    }
}