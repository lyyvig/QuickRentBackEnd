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
    public class ColorManager : IColorService {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal) {
            _colorDal = colorDal;
        }

        [SecuredOperation("admin,color.all,color.add")]
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color) {
            var businessResult = BusinessRules.Run(CheckIfColorExists(color));
            if (businessResult != null)
                return businessResult;
            
            _colorDal.Add(color);
            return new SuccessResult();
        }

        [SecuredOperation("admin,color.all,color.delete")]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color) {
            _colorDal.Delete(color);
            return new SuccessResult();
        }

        [CacheAspect(10)]
        public IDataResult<Color> Get(int colorId) {
            return new SuccessDataResult<Color>(_colorDal.Get(b => b.Id == colorId));
        }

        [CacheAspect(10)]
        public IDataResult<List<Color>> GetAll() {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        //[SecuredOperation("admin,color.all,color.update")]
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color) {
            _colorDal.Update(color);
            return new SuccessResult();
        }

        private IResult CheckIfColorExists(Color color) {
            if (_colorDal.Get(c => c.Name == color.Name) != null) {
                return new ErrorResult(Messages.ColorAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
