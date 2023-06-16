
using Core.CEntities.Dtos;
using Core.CEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CSecurity
{
    public interface ITokenHelper
    {
        LoginResponseDto GenerateJwtToken(User user, string userType, List<RoleClaim> claims);
        public string GenerateRefreshToken(string ipAddress);
        //public int? ValidateJwtToken(string token);
    }
}
