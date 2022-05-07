using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class RentalManager : IRentalService {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal) {
            _rentalDal = rentalDal;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental) {
            if (_rentalDal.IsRented(rental.CarId)) return new ErrorResult(Messages.CarAlreadyRented);
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.ItemAdded + rental.Id);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental) {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.ItemDeleted + rental.Id);
        }

        [CacheAspect(10)]
        public IDataResult<Rental> Get(int userId) {
            return new SuccessDataResult<Rental>(_rentalDal.Get(u => u.Id == userId));
        }

        [CacheAspect(10)]
        public IDataResult<List<Rental>> GetAll() {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental) {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.ItemUpdated + rental.Id);
        }
    }
}
