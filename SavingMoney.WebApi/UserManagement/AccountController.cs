using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<OrgUser> _userManager;

        public AccountController(UserManager<OrgUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpPost]
        public IActionResult Create(UserRegisterModel user)
        {
            return Ok();
        }
    }
}