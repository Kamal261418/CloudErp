using System.ComponentModel.DataAnnotations;

namespace CLOUD.API.Controllers
{
    public class UserDto 
    {
      public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
