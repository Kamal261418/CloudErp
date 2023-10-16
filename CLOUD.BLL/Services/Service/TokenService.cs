
using CLOUD.API.Controllers;
using CLOUD.BLL.Dtos;
using CLOUD.BLL.Enterfaces;
using CLOUD.BLL.Services;
using CLOUD.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CLOUD.BLL.Services.TokenService
{
   

    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private  UserManager<ApplicationUser> usermanager;
        private readonly JWT _jwt;



        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> usermanager, IOptions<JWT> jwt)
        {
            this.configuration = configuration;
            this.usermanager = usermanager;
            _jwt = jwt.Value;
        }
        public async Task<object> RegisterAsync(RegisterModel model)
        {
            if (await usermanager.FindByEmailAsync(model.Email) is not null)
                return new  { Message = "Email is already registered!" };

            if (await usermanager.FindByNameAsync(model.Username) is not null)
                return new  { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new   { Message = errors };
            }

            await usermanager.AddToRoleAsync(user, "User");


            return new 
            {
                user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Username = user.UserName
            };
        }

        //claims for gettinng strong key 
        public async Task<ResponseModel> CreateToken(LoginData user)
        {
            var data=await usermanager.FindByEmailAsync(user.Email);
            
          
            if(data is not null&& await usermanager.CheckPasswordAsync(data, user.Password))
            {
                var authClaims = new List<Claim>()
          {
              new Claim(ClaimTypes.Email,data.Email),
              new Claim(ClaimTypes.GivenName,data.FirstName+" "+data.LastName),
          };


                var userRoles = await usermanager.GetRolesAsync(data);
                foreach (var role in userRoles)
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));




                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

                var signingCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256);



                var token = new JwtSecurityToken(

                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                    claims: authClaims,
                    signingCredentials: signingCredentials
                    );

                return new ResponseModel { Data = new {Token= new JwtSecurityTokenHandler().WriteToken(token),ExpireDate=token.ValidTo,IsAuthenticated=true } ,StatusCode=200,IsSucceded=true};

            }
            return new ResponseModel { StatusCode = 400 };
          
        }


    }
}

