using Business.Abstract;
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
        [SecuredOperation("admin")]
        public IDataResult<int> Add(Car car) {
            _carDal.Add(car);
            car = _carDal.Get(c =>
                c.BrandId == car.BrandId &&
                c.ColorId == car.ColorId &&
                c.Model == car.Model &&
                c.ModelYear == car.ModelYear
            );
            return new SuccessDataResult<int>(car.Id, Messages.CarAdded);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(Car car) {
            _carImageService.DeleteByCarId(car.Id);
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<Car> Get(int id) {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<CarDetailDto> GetCarDetail(int carId) {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetail(carId));
        }


        public IDataResult<List<Car>> GetAll() {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<List<CarDetailDto>> GetDetails() {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetDetailsByFilter(FilterOptions filter) {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails( car =>
                    (filter.BrandId == null || filter.BrandId == car.BrandId) &&
                    (filter.ColorId == null || filter.ColorId == car.ColorId) &&
                    (filter.MinModelYear == null || filter.MinModelYear <= car.ModelYear) &&
                    (filter.MinPrice == null || filter.MinPrice <= car.DailyPrice) &&
                    (filter.MaxPrice == null || filter.MaxPrice >= car.DailyPrice)
                ));
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("admin")]
        public IResult Update(Car car) {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        [SecuredOperation("admin")]
        public IDataResult<CarStatsDto> GetCarStats(int id) {
            return new SuccessDataResult<CarStatsDto>(_carDal.GetCarStats(id));
        }
    }
}
