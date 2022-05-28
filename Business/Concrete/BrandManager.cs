using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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

        //[SecuredOperation("admin,brand.all,brand.add")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand) {
            var businessResult = BusinessRules.Run(CheckIfBrandExists(brand));
            if (businessResult != null) {
                return businessResult;
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.ItemAdded + brand.Name);
        }

        [SecuredOperation("admin,brand.all,brand.delete")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand) {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.ItemDeleted + brand.Name);
        }

        [CacheAspect(10)]
        public IDataResult<Brand> Get(int brandId) {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId), Messages.ItemListed);
        }

        [CacheAspect(10)]
        public IDataResult<List<Brand>> GetAll() {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.ItemsListed);
        }
        
        //[SecuredOperation("admin,brand.all,brand.update")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand) {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.ItemUpdated + brand.Name);
        }

        private IResult CheckIfBrandExists(Brand brand) {
            if (_brandDal.Get(b => b.Name == brand.Name) != null) {
                return new ErrorResult(Messages.BrandAlreadyExists);
            }
            return new SuccessResult();
        }

    }
}
