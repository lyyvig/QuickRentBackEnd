using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color) {
            _colorDal.Add(color);
            return new SuccessResult(Messages.ItemAdded + color.Name);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color) {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ItemDeleted + color.Name);
        }

        [CacheAspect(10)]
        public IDataResult<Color> Get(int colorId) {
            return new SuccessDataResult<Color>(_colorDal.Get(b => b.Id == colorId), Messages.ItemListed);
        }

        [CacheAspect(10)]
        public IDataResult<List<Color>> GetAll() {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ItemsListed);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color) {
            _colorDal.Update(color);
            return new SuccessResult(Messages.ItemUpdated + color.Name);
        }
    }
}
