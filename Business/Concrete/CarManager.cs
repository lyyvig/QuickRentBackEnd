using Business.Abstract;
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

        public void Add(Car car) {
            if (car.Description.Length < 2) throw new Exception("Description length under limit");
            if (car.DailyPrice <= 0) throw new Exception("Daily price must he higher than 0");
            _carDal.Add(car);
        }

        public void Delete(Car car) {
            _carDal.Delete(car);
        }

        public Car Get(int id) {
            return _carDal.Get(c => c.Id == id);
        }

        public List<Car> GetAll() {
            return _carDal.GetAll();
        }

        public List<CarDetailDto> GetCarDetails() {
            return _carDal.GetCarDetails();
        }

        public List<Car> GetCarsByBrandId(int brandId) {
            return _carDal.GetAll(p => p.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId) {
            return _carDal.GetAll(p => p.ColorId == colorId);
        }

        public void Update(Car car) {
            _carDal.Update(car);
        }
    }
}
