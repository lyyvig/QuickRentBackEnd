﻿using Business.Abstract;
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

        public RentalManager(IRentalDal rentalDal) {
            _rentalDal = rentalDal;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental) {
            var businessResult = BusinessRules.Run(CheckIfCarAlreadyRented(rental.CarId));
            if(businessResult != null) {
                return businessResult;
            }

            rental.RentDate = DateTime.Now;
            rental.ReturnDate = null;

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

        public IResult ReturnCar(Rental rental) {
            var rentalToUpdate = _rentalDal.Get(r => r.Id == rental.Id);
            rentalToUpdate.ReturnDate = DateTime.Now;
            return Update(rentalToUpdate);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental) {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.ItemUpdated + rental.Id);
        }

        private IResult CheckIfCarAlreadyRented(int carId) {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null).Any();
            if (result) {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
