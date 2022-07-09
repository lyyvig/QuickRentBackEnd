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
        IFindexService _findexService;
        ICustomerService _customerService;
        public RentalManager(IRentalDal rentalDal, IPaymentService paymentService, ICarService carService, IFindexService findexService, ICustomerService customerService) {
            _rentalDal = rentalDal;
            _paymentService = paymentService;
            _carService = carService;
            _findexService = findexService;
            _customerService = customerService;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental, CreditCard card) {
            var businessResult = BusinessRules.Run(
                CheckIfCarRented(rental),
                CheckIfFindexScoreSufficient(rental),
                CheckIfReturnDateGreaterThanRentDate(rental)
                );
            if (businessResult != null) {
                return businessResult;
            }
            rental.TotalDays = rental.ReturnDate.Subtract(rental.RentDate).Days + 1;
            rental.TotalPrice = rental.TotalDays * _carService.Get(rental.CarId).Data.DailyPrice;

            var paymentResult = _paymentService.Pay(card, rental.TotalPrice);
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

        [SecuredOperation("admin,rental.all,rental.getall")]
        [CacheAspect(10)]
        public IDataResult<List<Rental>> GetAll() {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<DateTime>> GetOccupiedDates(int carId) {
            List<DateTime> occupiedDates = new List<DateTime>();

            DateTime date = DateTime.Today;
            var lastDate = date.AddDays(30);

            var rentals = _rentalDal.GetAll(r =>
                r.CarId == carId &&
                r.ReturnDate > DateTime.Today
            );

            for (; date < lastDate; date = date.AddDays(1)) {
                foreach (var rental in rentals) {
                    if (date >= rental.RentDate && date <= rental.ReturnDate) {
                        occupiedDates.Add(date);
                        break;

                    }
                }
            }
            return new SuccessDataResult<List<DateTime>>(occupiedDates);

        }

        [CacheAspect(10)]
        public IDataResult<List<RentalDetailDto>> GetDetails() {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails().OrderByDescending(r => r.RentDate).ToList());
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

        private IResult CheckIfReturnDateGreaterThanRentDate(Rental rental) {
            if (rental.ReturnDate < rental.RentDate) {
                return new ErrorResult(Messages.ReturnDateLessThanRentDate);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarRented(Rental rental) {
            var isOccupied = _rentalDal.GetAll(r => r.CarId == rental.CarId &&
                r.ReturnDate >= rental.RentDate && //Finds the rentals which are not yet returned
                r.RentDate <= rental.ReturnDate // Finds the rentals which are rented before requests return date. Which means it occupies requests rent interval
            ).Any();
            if (isOccupied) {
                return new ErrorResult(Messages.CarAlreadyRented);
            }
            return new SuccessResult();
        }

        private IResult CheckIfFindexScoreSufficient(Rental rental) {
            var customer = _customerService.Get(rental.CustomerId).Data;
            if (customer.NationalIdentity == null) {
                return new ErrorResult(Messages.CustomerHasNoNationalIdentity);
            }
            var findexScore = _findexService.GetCustomerFindexScore(customer.NationalIdentity).Data;

            var carMinFindexScore = _carService.Get(rental.CarId).Data.FindexScore;
            if (findexScore < carMinFindexScore) {
                return new ErrorResult(Messages.FindexScoreInsufficient);
            }
            return new SuccessResult();
        }


    }
}
