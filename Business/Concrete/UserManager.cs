using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class UserManager : IUserService {
        IUserDal _userDal;
        ICustomerService _customerService;

        public UserManager(IUserDal userDal, ICustomerService customerService) {
            _userDal = userDal;
            _customerService = customerService;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user) {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user) {
            _userDal.Add(user);
            _customerService.Add(new Customer {
                Id = user.Id
            });
            return new SuccessResult();
        }

        public IResult Update(UserDto user) {
            var userToUpdate = GetByMail(user.Email).Data;

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            _userDal.Update(userToUpdate);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public IDataResult<List<UserDto>> GetUsers() {
            var result = _userDal.GetUsers();
            return new SuccessDataResult<List<UserDto>>(result);
        }

        public IDataResult<User> GetByMail(string email) {
            var result = _userDal.Get(u => u.Email == email);
            if (result != null) {
                return new SuccessDataResult<User>(result);
            }
            return new ErrorDataResult<User>(Messages.UserNotExists);
        }

        public IDataResult<UserDto> GetUser(int id) {
            var result = _userDal.Get(u => u.Id == id);
            if (result != null) {
                return new SuccessDataResult<UserDto>(new UserDto {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email
                });
            }
            return new ErrorDataResult<UserDto>(Messages.UserNotExists);
        }

        public IResult ChangePassword(ChangePasswordDto user) {
            var userToUpdate = GetByMail(user.Email).Data;

            if (HashingHelper.VerifyPasswordHash(user.OldPassword, userToUpdate.PasswordHash, userToUpdate.PasswordSalt)) {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(user.NewPassword, out passwordHash, out passwordSalt);
                userToUpdate.PasswordHash = passwordHash;
                userToUpdate.PasswordSalt = passwordSalt;
                _userDal.Update(userToUpdate);
                return new SuccessResult();
            }
            return new ErrorResult(Messages.PasswordError);
        }

        
    }
}
