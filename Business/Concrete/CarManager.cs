using Business.Abstract;
using Business.Constants;
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

        public CarManager(ICarDal carDal) {
            _carDal = carDal;
        }

        public IResult Add(Car car) {
            if (car.Description.Length < 2) return new ErrorResult(Messages.ItemNameInvalid);
            if (car.DailyPrice <= 0) return new ErrorResult(Messages.ItemDailyPriceInvalid);
            _carDal.Add(car);
            return new SuccessResult(Messages.ItemAdded + car.Description);
        }

        public IResult Delete(Car car) {
            _carDal.Delete(car);
            return new SuccessResult(Messages.ItemDeleted + car.Description);
        }

        public IDataResult<Car> Get(int id) {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.ItemListed);
        }

        public IDataResult<List<Car>> GetAll() {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.ItemsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails() {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.DetailedItemsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == brandId), Messages.ItemsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == colorId), Messages.ItemsListed);
        }

        public IResult Update(Car car) {
            _carDal.Update(car);
            return new SuccessResult(Messages.ItemUpdated + car.Description);
        }
    }
}
