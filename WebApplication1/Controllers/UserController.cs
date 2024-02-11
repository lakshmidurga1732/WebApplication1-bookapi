using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Entity;
using WebApplication1.Services;
using WebApplication1.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult AddUser(User user)
        {
            try
            {
                _userService.CreateUser(user);
                return Ok("User created successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("EditUser")]
        public IActionResult EditUser(User user)
        {
            try
            {
                _userService.EditUser(user);
                return Ok("User updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetUserById/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                User user = _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                // Convert userId to string
                string userIdString = userId.ToString();

                // Call DeleteUser method with the string userId
                _userService.DeleteUser(userIdString);

                return Ok("User deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = _userService.ValidateUser(login.Email, login.Password);
                AuthResponse authReponse = new AuthResponse();
                if (user != null)
                {
                    authReponse.UserId = user.UserId;
                    authReponse.UserName = user.UserName;
                    authReponse.Role = user.Role;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.Message);
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                         new Claim("UserName", user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email,user.Email),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

       

        
        
    

    
    }
}

