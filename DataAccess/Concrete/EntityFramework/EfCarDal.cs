using Castle.Core.Internal;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentDbContext>, ICarDal {
        public CarDetailDto GetCarDetail(int carId) {
            using (CarRentDbContext context = new CarRentDbContext()) {
                var result = from car in context.Cars
                             where car.Id == carId
                             join col in context.Colors on car.ColorId equals col.Id
                             join brd in context.Brands on car.BrandId equals brd.Id
                             select new CarDetailDto {
                                 Id = car.Id,
                                 BrandId = car.BrandId,
                                 BrandName = brd.Name,
                                 ColorId = car.ColorId,
                                 ColorName = col.Name,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 Images = context.CarImages.Where(ci => ci.CarId == car.Id).ToList()
                             };
                var carDetail = result.First();
                if (carDetail.Images.Count == 0)
                    carDetail.Images = new List<CarImage> { new CarImage {
                        CarId = carDetail.Id,
                        ImagePath = "images/default.jpg"
                    } };
                return carDetail;
            }
        }

        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null) {
            using (CarRentDbContext context = new CarRentDbContext()) {
                var result = from car in context.Cars
                             join col in context.Colors on car.ColorId equals col.Id
                             join brd in context.Brands on car.BrandId equals brd.Id
                             select new CarDetailDto {
                                 Id = car.Id,
                                 BrandId = car.BrandId,
                                 BrandName = brd.Name,
                                 ColorId = car.ColorId,
                                 ColorName = col.Name,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 Images = context.CarImages.Count(ci => ci.CarId == car.Id) != 0
                                 ? context.CarImages.Where(ci => ci.CarId == car.Id).ToList()
                                 : new List<CarImage> { new CarImage {
                                        CarId = car.Id,
                                        ImagePath = "images/default.jpg"
                                    } }

                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
