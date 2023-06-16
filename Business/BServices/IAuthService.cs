
using Core.CEntities.Dtos;
using Core.CResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BServices
{
    public interface IAuthService
    {
        IDataResult<LoginResponseDto> Login(LoginDto loginDto, string requestUrl);
    }
}
