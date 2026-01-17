using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturangDB_API.Data;
using ResturangDB_API.Models.DTOs.User;

namespace ResturangDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ResturangContext _context;
        public AuthController(ResturangContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login loginRequest)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginRequest.Username && u.PasswordHash == loginRequest.Password);

            if (user != null)
            {
                return Ok(new { Token = "fake-jwt-token" });
            }

            return Unauthorized();
        }
    }
}
