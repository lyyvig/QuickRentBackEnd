using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfUserDal : EfEntityRepositoryBase<User, CarRentDbContext>, IUserDal {
        public List<OperationClaim> GetClaims(User user) {
            using (var context = new CarRentDbContext()) {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public List<UserDto> GetUsers() {
            using (var context = new CarRentDbContext()) {
                var result = from user in context.Users
                             select new UserDto { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName };
                return result.ToList();
            }
        }
    }
}
