using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentDbContext>, IRentalDal {
        public List<RentalDetailDto> GetRentalDetails() {
            using (CarRentDbContext context = new()) {
                var result = from rental in context.Rentals
                             join car in context.Cars on rental.CarId equals car.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join user in context.Users on rental.CustomerId equals user.Id
                             select new RentalDetailDto {
                                 Id = rental.Id,
                                 BrandName = brand.Name,
                                 CustomerName = user.FirstName + user.LastName,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
