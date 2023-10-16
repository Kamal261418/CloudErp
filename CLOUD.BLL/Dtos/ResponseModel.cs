using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLOUD.BLL.Dtos
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public object? Data { get; set; }
        public bool IsSucceded { get; set; }=false;

    }
}
