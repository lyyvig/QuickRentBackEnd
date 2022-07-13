using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Business;
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

        public IDataResult<List<OperationClaim>> GetUserClaims(int userId) {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetUserClaims(userId));
        }

        [SecuredOperation("admin")]
        public IDataResult<List<OperationClaim>> GetOperationClaims() {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetOperationClaims());
            
        }

        [SecuredOperation("admin")]
        public IResult AddClaim(UserOperationClaim operationClaim) {
            var businessResult = BusinessRules.Run(
                    CheckIfAlreadyHaveClaim(operationClaim)
                );
            if (businessResult != null) {
                return businessResult;
            }
            _userDal.AddClaim(operationClaim);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public IResult DeleteClaim(UserOperationClaim operationClaim) {
            _userDal.DeleteClaim(operationClaim);
            return new SuccessResult();
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
            return new SuccessResult(Messages.InformationUpdated);
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
            return new ErrorDataResult<User>(Messages.UserDoesntExists);
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
            return new ErrorDataResult<UserDto>(Messages.UserDoesntExists);
        }

        public IResult ChangePassword(ChangePasswordDto user) {
            var userToUpdate = GetByMail(user.Email).Data;

            if (HashingHelper.VerifyPasswordHash(user.OldPassword, userToUpdate.PasswordHash, userToUpdate.PasswordSalt)) {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(user.NewPassword, out passwordHash, out passwordSalt);
                userToUpdate.PasswordHash = passwordHash;
                userToUpdate.PasswordSalt = passwordSalt;
                _userDal.Update(userToUpdate);
                return new SuccessResult(Messages.PasswordUpdated);
            }
            return new ErrorResult(Messages.PasswordError);
        }

        private IResult CheckIfAlreadyHaveClaim(UserOperationClaim operationClaim) {
            var claims = GetUserClaims(operationClaim.UserId);
            if (claims.Data.Any(c => operationClaim.OperationClaimId == c.Id)) {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        
    }
}
