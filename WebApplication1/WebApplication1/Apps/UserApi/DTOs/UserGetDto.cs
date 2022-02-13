using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Apps.UserApi.DTOs
{
    public class UserGetDto
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
