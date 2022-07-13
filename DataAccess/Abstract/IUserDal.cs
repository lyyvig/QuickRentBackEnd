using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract {
    public interface IUserDal : IEntityRepository<User> {
        List<OperationClaim> GetUserClaims(int userId);
        List<UserDto> GetUsers();
        List<OperationClaim> GetOperationClaims();
        void AddClaim(UserOperationClaim operationClaim);
        void DeleteClaim(UserOperationClaim operationClaim);
    }
}
