using Business.BMessages;
using Core.CEntities.Dtos;
using Core.CResult;
using Core.CSecurity;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BServices
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ISchoolService _schoolService;
        private ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, ISchoolService schoolService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _schoolService = schoolService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<LoginResponseDto> Login(LoginDto loginDto, string requestUrl)
        {
            // get schoolId+++
            var schoolId = _schoolService.GetByUrl(requestUrl);
            if (schoolId == 0)
            {
                return new ErrorDataResult<LoginResponseDto>(Messages.SchollNotFound);
            }

            // validate user
            var userType = userTypeCheck(loginDto.Password);
            var user = _userService.GetByPhone(loginDto.Phone, userType, schoolId);
            if (user == null)
            {
                return new ErrorDataResult<LoginResponseDto>(Messages.UserNotFound); //("Phone or password is incorrect");
            }

            // validate pass
            var passwordToCheck = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (passwordToCheck == false)
            {
                return new ErrorDataResult<LoginResponseDto>(Messages.UserPasswordError);
            }

            // authentication successful so generate jwt (with claims) and refresh tokens
            var claims = _userService.GetClaims(user);
            var loginResponseDto = _tokenHelper.GenerateJwtToken(user, userType, claims);


            using (var context = new KidBackContext())
            {
                var refreshToken = context.Users.FirstOrDefault(u => u.Id == user.Id);
                refreshToken.RefreshToken = loginResponseDto.RefreshToken;
                refreshToken.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                context.Update(refreshToken);
                context.SaveChanges();
            }
            return new SuccessDataResult<LoginResponseDto>(loginResponseDto, Messages.UserAccessTokenCreated);
        }

        /*
        Advisor şifresi @ ile başlar (1)
        Staff şifresi * ile başlar (2)
        Parent şifreleri rakamlardan oluşur (4)
        */
        private string userTypeCheck(string password)
        {
            if (password.Substring(0, 1) == "@")
            {
                return "A"; // Advisor
            }
            else if (password.Substring(0, 1) == "*")
            {
                return "S"; // Staff
            }
            else if (password.Substring(0, 1) == "0" || password.Substring(0, 1) == "1" || password.Substring(0, 1) == "2" || password.Substring(0, 1) == "3" || password.Substring(0, 1) == "4" || password.Substring(0, 1) == "5" || password.Substring(0, 1) == "6" || password.Substring(0, 1) == "7" || password.Substring(0, 1) == "8" || password.Substring(0, 1) == "9")
            {
                return "P"; // Parent
            }
            else
            {
                return "X"; // Parent
            }
        }
    }
}
