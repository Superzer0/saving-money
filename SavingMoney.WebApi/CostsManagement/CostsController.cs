using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SavingMoney.WebApi.CostsManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCost()
        {
            return Ok();
        }
        
    }
}