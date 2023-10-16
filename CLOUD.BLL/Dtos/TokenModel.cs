using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLOUD.BLL.Dtos
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool IsAuthenticated { get; set; }=false;
    }
}
