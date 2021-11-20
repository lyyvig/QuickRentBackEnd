using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentDbContext>, IRentalDal {
        public bool IsRented(int carId) {
            using (CarRentDbContext context = new CarRentDbContext()) {
                return context.Rentals.Any(r => r.CarId == carId && r.ReturnDate == DateTime.MinValue);
            }
        }
    }
}
