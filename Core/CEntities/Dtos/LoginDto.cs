using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CEntities.Dtos
{
    public class LoginDto : IDto
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
    }
}
