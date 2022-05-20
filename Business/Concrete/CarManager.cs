﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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
    public class CarManager : ICarService {
        ICarDal _carDal;
        ICarImageService _carImageService;
        public CarManager(ICarDal carDal, ICarImageService carImageService) {
            _carDal = carDal;
            _carImageService = carImageService;

        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car) {
            _carDal.Add(car);
            return new SuccessResult(Messages.ItemAdded + car.Description);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car) {
            _carImageService.DeleteByCarId(car.Id);
            _carDal.Delete(car);
            return new SuccessResult(Messages.ItemDeleted + car.Description);
        }

        public IDataResult<Car> Get(int id) {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.ItemListed);
        }

        public IDataResult<CarDetailDto> GetCarDetail(int carId) {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetail(carId));
        }


        public IDataResult<List<Car>> GetAll() {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.ItemsListed);
        }

        public IDataResult<List<CarDetailDto>> GetDetails() {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.DetailedItemsListed);
        }

        public IDataResult<List<CarDetailDto>> GetDetailsByBrandId(int brandId) {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(cd => cd.BrandId == brandId), Messages.DetailedItemsListed);
        }

        public IDataResult<List<CarDetailDto>> GetDetailsByColorId(int colorId) {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(cd => cd.ColorId == colorId), Messages.DetailedItemsListed);
        }

        [CacheAspect(10)]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == brandId), Messages.ItemsListed);
        }

        [CacheAspect(10)]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == colorId), Messages.ItemsListed);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car) {
            _carDal.Update(car);
            return new SuccessResult(Messages.ItemUpdated + car.Description);
        }
    }
}
