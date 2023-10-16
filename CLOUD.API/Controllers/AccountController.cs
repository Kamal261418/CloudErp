


using AutoMapper;
using CLOUD.API.Helpers.ErrorHandeler;
using CLOUD.BLL.Dtos;
using CLOUD.BLL.Enterfaces;
using CLOUD.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLOUD.API.Controllers
{
   
 
    public class AccountController :BaseController
    {

        private readonly ITokenService _tokenservice;

        public AccountController(ITokenService tokenservice)
        {
     
            _tokenservice = tokenservice;
        }
      
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenservice.RegisterAsync(model);



            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginData logindata)
        {
            var result=await _tokenservice.CreateToken(logindata); 
            return Ok(result);
        }


        /*
         Form 
        Segme
        Query String 
         */









        }
}