using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class CustomerManager : ICustomerService {
        ICustomerDal _customerDal;
        IUserDal _userDal;

        public CustomerManager(ICustomerDal customerDal, IUserDal userDal) {
            _customerDal = customerDal;
            _userDal = userDal;
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer) {
            var result = BusinessRules.Run(UserExist(customer.Id));
            if (result != null) return result;

            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer) {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [CacheAspect(10)]
        public IDataResult<Customer> Get(int userId) {
            return new SuccessDataResult<Customer>(_customerDal.Get(u => u.Id == userId));
        }

        [SecuredOperation("admin")]
        [CacheAspect(10)]
        public IDataResult<List<Customer>> GetAll() {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer) {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.InformationUpdated);
        }

        private IResult UserExist(int id) {
            if(_userDal.Get(u => u.Id == id) != null) return new SuccessResult();
            return new ErrorResult(Messages.UserDoesntExists);
        }
    }
}
