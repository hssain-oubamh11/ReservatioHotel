using Microsoft.AspNetCore.Mvc;

namespace SystemGestionReservation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Connexion réussie !");
        }
    }
}