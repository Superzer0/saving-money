using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SavingMoney.WebApi.Categories
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CostCategoriesController : ControllerBase
    {
        /// <summary>
        /// Creates cost category within organization
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns the newly created category</response>
        /// <response code="400">There are validation errors for model</response>
        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCategory()
        {
            return Ok();
        }
        
        
        /// <summary>
        /// Creates cost sub category within category
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "CreateSubCategory")]
        public IActionResult CreateSubCategory()
        {
            return Ok();
        }
    }
}