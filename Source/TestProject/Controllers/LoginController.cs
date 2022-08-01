using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TestProject.Data.UserRepo;
using TestProject.ViewModels;

namespace TestProject.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private IConfiguration _config;
        private IUser _repo;
        private IMapper _mapper;
        public LoginController(IConfiguration config, IUser repo, IMapper mapper) {
            _config = config;
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel userLogin) {
            var user = Authenticate(userLogin);

            if(user != null) {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(UserViewModel user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserViewModel Authenticate(LoginViewModel userLogin) {
            var currentUser = _mapper.Map<UserViewModel>(_repo.Set().FirstOrDefault(o => o.EmailAddress.ToLower() == userLogin.Login.ToLower() && o.Password == userLogin.Password));

            if(currentUser != null) {
                return currentUser;
            }

            return null;
        }
    }
}
