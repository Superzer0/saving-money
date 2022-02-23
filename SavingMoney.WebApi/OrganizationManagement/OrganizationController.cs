using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SavingMoney.WebApi.Controllers;

namespace SavingMoney.WebApi.OrganizationManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        /// <summary>
        /// Creates organization along with its first member. Default cost categories are created and attached
        /// </summary>
        /// <param name="orgCreateModel"></param>
        /// <returns></returns>
        /// <response code="201">If the organization was created successfully </response>
        /// <response code="400">If there were model validation problems</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult CreateOrganization(OrgCreateModel orgCreateModel)
        {
            
            
            
            return Ok();
        }
        
    }
}