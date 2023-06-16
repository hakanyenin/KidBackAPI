using Core.CEntities.Models;
using Core.CResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BServices
{
    public interface IUserService
    {
        IResult Add(User user);
        User GetByPhone(string phone, string userType, int schoolId);
        IDataResult<User> GetById(int id);
        List<RoleClaim> GetClaims(User user);
        IDataResult<List<User>> GetList();
    }
}
