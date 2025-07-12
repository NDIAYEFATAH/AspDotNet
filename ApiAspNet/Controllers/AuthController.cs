using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiAspNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Cette route est protégée : l'utilisateur doit avoir un token JWT valide
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            // Par exemple récupérer le username (claim 'preferred_username' dans Keycloak)
            var username = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return Ok(new
            {
                Username = username,
                Email = email,
                Claims = claims
            });
        }
    }
}
