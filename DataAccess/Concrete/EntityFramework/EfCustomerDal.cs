using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentDbContext>, ICustomerDal {
        public bool UserExist(int userId) {
            using (CarRentDbContext context = new CarRentDbContext()) {
                return context.Users.Any(u => u.Id == userId);
            }
        }
    }
}
