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
        public List<CarDetailDto> GetCarDetails() {
            using (CarRentDbContext context = new CarRentDbContext()) {
                var result = from car in context.Cars
                             join col in context.Colors
                             on car.ColorId equals col.Id
                             join brd in context.Brands
                             on car.BrandId equals brd.Id
                             select new CarDetailDto { CarName = car.Description, BrandName = brd.Name, ColorName = col.Name, DailyPrice = car.DailyPrice };
                return result.ToList();
            }
        }
    }
}
