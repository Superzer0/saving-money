using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SavingMoney.WebApi.Controllers;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.OrganizationManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        
        /// <summary>
        /// Creates organization along with its first member. Default cost categories are created and attached
        /// </summary>
        /// <param name="orgCreateModel"></param>
        /// <returns></returns>
        /// <response code="200">If the organization was created successfully </response>
        /// <response code="400">If there were model validation problems</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(typeof(OrgCreatedResponse))]
        public async Task<IActionResult> CreateOrganization(OrgCreateModel orgCreateModel)
        {
            try
            {
                var createdOrg = await _organizationService.CreateOrganization(orgCreateModel);
                return Ok(new OrgCreatedResponse
                {
                    OrgId = createdOrg.Id,
                    FirstUserId = createdOrg.OrganizationUsers.First().Id
                });
            }
            catch (OrgValidationException e)
            {
                if (e.ValidationModel.OrgNameTaken)
                {
                    ModelState.AddModelError<OrgValidationModel>(p => p.OrgNameTaken, "Organization name is taken");    
                }
                if (e.ValidationModel.UserEmailTaken)
                {
                    ModelState.AddModelError<OrgValidationModel>(p => p.UserEmailTaken, "User email is taken");    
                }
                if (e.ValidationModel.NewUserValidation.Any())
                {
                    foreach (var identityError in e.ValidationModel.NewUserValidation)
                    {
                        ModelState.AddModelError<OrgValidationModel>(p => p.NewUserValidation, identityError.Code);    
                    }
                }
                
                return ValidationProblem();
            }
        }
    }
}