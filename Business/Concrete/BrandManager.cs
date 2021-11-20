using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class BrandManager : IBrandService {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal) {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand) {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.ItemAdded + brand.Name);
        }

        public IResult Delete(Brand brand) {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.ItemDeleted + brand.Name);
        }

        public IDataResult<Brand> Get(int brandId) {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId), Messages.ItemListed);
        }

        public IDataResult<List<Brand>> GetAll() {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.ItemsListed);
        }

        public IResult Update(Brand brand) {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.ItemUpdated + brand.Name);
        }
    }
}
