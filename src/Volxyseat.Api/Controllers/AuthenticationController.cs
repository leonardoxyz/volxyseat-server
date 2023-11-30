using Microsoft.IdentityModel.JsonWebTokens;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Volxyseat.Infrastructure.Configurations;
using Volxyseat.Domain.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Volxyseat.Domain.Models.AuthModel;
using Volxyseat.Domain.Models.Transaction;

namespace Volxyseat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private List<InvalidToken> _invalidTokens = new List<InvalidToken>();
        private readonly ITransactionRepository _transactionRepository;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            JwtConfig jwtConfig,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            ITransactionRepository transactionRepository
        )
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
            _configuration = configuration;
            _signInManager = signInManager;
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] ClientViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newUser = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.CompanyName
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(newUser);
                return Ok(new { token });
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
                if(existingUser == null)
                {
                    return BadRequest();
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
                var transaction = GetByClientId(Guid.Parse(existingUser.Id));

                if (!isCorrect)
                {
                    return BadRequest();
                }

                

                Guid? transactionId = transaction == null ? null : transaction.Id;

                var jwtToken = this.GenerateJwtToken(existingUser);
                return Ok(new { token = jwtToken, username = existingUser.UserName, transaction = transactionId, email = existingUser.Email});
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            _invalidTokens.Add(new InvalidToken { TokenId = token });

            return Ok(new { message = "Logout bem-sucedido" });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{clientId}")]
        public TransactionModel? GetByClientId(Guid clientId)
        {
            var trasaction = _transactionRepository.GetByClientId(clientId);

            if (trasaction == null)
            {
                return null;
            }

            return trasaction;
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                }),

                Expires = DateTime.Now.ToUniversalTime().AddHours(2),
                NotBefore = DateTime.Now.ToUniversalTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
