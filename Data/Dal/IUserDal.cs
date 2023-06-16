using Core.CData;
using Core.CEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dal
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<RoleClaim> GetClaims(User user);
    }
}
