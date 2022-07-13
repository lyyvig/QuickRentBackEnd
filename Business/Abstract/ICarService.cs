using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract {
    public interface ICarService {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> Get(int id);
        IDataResult<CarDetailDto> GetCarDetail(int carId);
        IDataResult<List<CarDetailDto>> GetDetails();
        IDataResult<List<CarDetailDto>> GetDetailsByFilter(FilterOptions filter);
        IDataResult<int> Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<CarStatsDto> GetCarStats(int id);
    }
}
