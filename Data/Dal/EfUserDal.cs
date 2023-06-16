using Core.CData;
using Core.CEntities.Models;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dal
{
    public class EfUserDal : EfEntityRepository<User, KidBackContext>, IUserDal
    {
        public List<RoleClaim> GetClaims(User user)
        {
            using (var context = new KidBackContext())
            {
                var result = from roleClaim in context.RoleClaims
                             join userRoleClaim in context.UserRoleClaims
                                 on roleClaim.Id equals userRoleClaim.ClaimId
                             where userRoleClaim.UserId == user.Id
                             select new RoleClaim { Id = roleClaim.Id, ClaimName = roleClaim.ClaimName };
                return result.ToList();
            }
        }
    }
}
