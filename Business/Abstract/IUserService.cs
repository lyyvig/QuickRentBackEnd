using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract {
    public interface IUserService {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<UserDto> GetUser(int id);
        IResult Add(User user);
        IResult Update(UserDto userForUpdate);
        IResult ChangePassword(ChangePasswordDto user);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<UserDto>> GetUsers();
    }
}
