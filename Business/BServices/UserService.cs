
using Business.BMessages;
using Core.CEntities.Models;
using Core.CResult;
using Data.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BServices
{
    public class UserService : IUserService
    {
        IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IDataResult<User> GetById(int id)
        {
            var userToCheck = _userDal.Get(u => u.Id == id && u.Status == true);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            return new SuccessDataResult<User>(userToCheck);
        }

        public User GetByPhone(string phone, string userType, int schoolId)
        {
            return _userDal.Get(u => u.Phone == phone && u.Status == true && u.UserTypeId == userType && u.SchoolId == schoolId);
        }

        public List<RoleClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetList().ToList());
        }
    }
}
