using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete {
    public class AuthManager : IAuthService {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper) {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto) {

            var result = BusinessRules.Run(CheckIfUserAlreadyExists(userForRegisterDto.Email));
            if(result != null) {
                return new ErrorDataResult<User>(result.Message);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto) {

            var result = BusinessRules.Run(CheckIfUserExists(userForLoginDto.Email));
            if(result != null) {
                return new ErrorDataResult<User>(result.Message);
            }

            var userToCheck = _userService.GetByMail(userForLoginDto.Email);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt)) {
                return new ErrorDataResult<User>(Messages.WrongPasswordOrEmail);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) {
            var claims = _userService.GetUserClaims(user.Id).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        private IResult CheckIfUserExists(string email) {
            if (_userService.GetByMail(email).Success) {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.WrongPasswordOrEmail);
        }

        private IResult CheckIfUserAlreadyExists(string email) {
            if (_userService.GetByMail(email).Success) {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

    }
}
