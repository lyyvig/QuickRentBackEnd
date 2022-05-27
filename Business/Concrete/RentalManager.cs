using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class RentalManager : IRentalService {
        IRentalDal _rentalDal;
        ICarService _carService;
        IPaymentService _paymentService;
        public RentalManager(IRentalDal rentalDal, IPaymentService paymentService, ICarService carService) {
            _rentalDal = rentalDal;
            _paymentService = paymentService;
            _carService = carService;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental, CreditCard card) {
            var businessResult = BusinessRules.Run(CheckIfCarRented(rental));
            if (businessResult != null) {
                return businessResult;
            }
            TimeSpan ts = rental.ReturnDate - rental.RentDate;
            int amount = ((int)ts.TotalDays + 1) * _carService.Get(rental.CarId).Data.DailyPrice;

            var paymentResult = _paymentService.Pay(card, amount);
            if (!paymentResult.Success) {
                return paymentResult;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.ItemAdded + rental.Id);
        }

        [SecuredOperation("admin,rental.all,rental.delete")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental) {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.ItemDeleted + rental.Id);
        }

        [CacheAspect(10)]
        public IDataResult<Rental> Get(int userId) {
            return new SuccessDataResult<Rental>(_rentalDal.Get(u => u.Id == userId));
        }

        //[SecuredOperation("admin,rental.all,rental.getall")]
        [CacheAspect(10)]
        public IDataResult<List<Rental>> GetAll() {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect(10)]
        public IDataResult<List<RentalDetailDto>> GetDetails() {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental) {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.ItemUpdated + rental.Id);
        }

        public IDataResult<bool> CheckIfCarAlreadyRented(Rental rentalRequest) {
            var isOccupied = CheckIfCarRented(rentalRequest);
            if (isOccupied.Success) {
                return new SuccessDataResult<bool>(true);
            }
            return new SuccessDataResult<bool>(false);
        }

        private IResult CheckIfCarRented(Rental rental) {
            var isOccupied = _rentalDal.GetAll(r => r.CarId == rental.CarId &&
                r.ReturnDate >= rental.RentDate && //Finds the rentals which are not yet returned
                r.RentDate <= rental.ReturnDate // Finds the rentals which are rented before requests return date. Which means it occupies requests rent interval
            ).Any();
            if (isOccupied) {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
