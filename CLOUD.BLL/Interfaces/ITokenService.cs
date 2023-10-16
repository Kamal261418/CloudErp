using CLOUD.API.Controllers;
using CLOUD.BLL.Dtos;
using CLOUD.DAL.Entities.Identity;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CLOUD.BLL.Enterfaces
{
    public interface ITokenService
    {
        public Task<object> RegisterAsync(RegisterModel model);
        public Task<ResponseModel> CreateToken(LoginData user);
    }
}
