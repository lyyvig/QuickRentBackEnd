using Business.Abstract;
using Business.Constants;
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

        public CustomerManager(ICustomerDal customerDal) {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer) {
            if (!_customerDal.UserExist(customer.Id)) return new ErrorResult(Messages.UserNotExist);
            _customerDal.Add(customer);
            return new SuccessResult(Messages.ItemAdded + customer.Id);
        }

        public IResult Delete(Customer customer) {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.ItemDeleted + customer.Id);
        }

        public IDataResult<Customer> Get(int userId) {
            return new SuccessDataResult<Customer>(_customerDal.Get(u => u.Id == userId));
        }

        public IDataResult<List<Customer>> GetAll() {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IResult Update(Customer customer) {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.ItemUpdated + customer.Id);
        }
    }
}
