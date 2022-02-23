using Microsoft.AspNetCore.Mvc;

namespace SavingMoney.WebApi.PredictionsManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMonthlyPrediction()
        {
            return Ok();
        }
    }
}