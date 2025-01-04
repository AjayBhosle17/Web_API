using _2_Method_Para_With_Crud_IN_API.Models;
using _2_Method_Para_With_Crud_IN_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace _2_Method_Para_With_Crud_IN_API.Controllers
{
    public class AccountController : ApiController
    {
        ProductDBContext _dBContext;

        public AccountController()
        {
            _dBContext = new ProductDBContext();
        }

        [HttpPost]
        public IHttpActionResult Register(Users user)
        {

            if (ModelState.IsValid)
            {


                _dBContext.Users.Add(user);
                _dBContext.SaveChanges();

                return Ok("User Created Successfully");
            }
            else
            {
                return BadRequest("Please Correct Input Request");
            }
        }

        [HttpPut]

        public IHttpActionResult Login(LoginModel login) { 
        
            bool isAuthenticated = _dBContext.Users.Any(u=>u.UserName==login.UserName
             &&  u.Password==login.Password);

            if (isAuthenticated) {
                string token = createToken(login.UserName);

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        // create token
        private string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(10); // token will expire in 10 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
                // can we add multiple
            });

            // security key

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var token = (JwtSecurityToken)
        tokenHandler.CreateJwtSecurityToken(issuer: "https://localhost:44371", audience: "https://localhost:44371",
            subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
}
}
