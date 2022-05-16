using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class UserManager : IUserService {
        IUserDal _userDal;

        public UserManager(IUserDal userDal) {
            _userDal = userDal;
        }

        [CacheAspect(60)]
        public IDataResult<List<OperationClaim>> GetClaims(User user) {
            return new SuccessDataResult<List<OperationClaim>>( _userDal.GetClaims(user));
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user) {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetByMail(string email) {
            var result = _userDal.Get(u => u.Email == email);
            if (result != null) {
                return new SuccessDataResult<User>(result);
            }
            return new ErrorDataResult<User>(Messages.UserNotExists);
        }
    }
}
