using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AccountServiceUser> _userManager;
        private readonly SignInManager<AccountServiceUser> _signInManager;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, SignInManager<AccountServiceUser> signInManager, UserManager<AccountServiceUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string email, string userName, string password)
        {
            var user = new AccountServiceUser()
            {
                Email = email,
                UserName = userName,
            };

            await _userManager.CreateAsync(user, password);

            return Ok();
        }

        [HttpGet("RecoveryToken")]
        public async Task<IActionResult> RequestRecovery(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(token);
        }

        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound(email);
            var recovery = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return recovery.Succeeded ? Ok():Problem(recovery.Errors.ToString());
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound(email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded ? Ok():Problem(result.ToString());
        }

    }
}