using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class CarImageManager : ICarImageService {
        ICarImageDal _carImageDal;
        

        public CarImageManager(ICarImageDal carImageDal) {
            _carImageDal = carImageDal;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile formFile, CarImage carImage) {
            var result = BusinessRules.Run(CheckIfImageCountOfCarExceeded(carImage.CarId));
            if(result != null) {
                return result;
            }

            string imageName = string.Format(@"{0}.jpg", Guid.NewGuid());
            carImage.ImagePath = Paths.CarImagePath + imageName;
            carImage.Date = DateTime.Now;

            FileHelper.Write(formFile, Paths.RootPath + carImage.ImagePath);


            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ItemAdded + carImage.Id);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage) {
            string path = _carImageDal.Get(ci => ci.Id == carImage.Id).ImagePath; 

            _carImageDal.Delete(carImage);
            FileHelper.Delete(Paths.RootPath + path); 


            return new SuccessResult(Messages.ItemDeleted + carImage.Id);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult DeleteByCarId(int carId) {
            var result = GetByCarId(carId);
            if (result.Success) {
                foreach (var item in result.Data) {
                    Delete(item);
                }
            }
            return new SuccessResult();
        }

        [SecuredOperation("admin,carimage.all,carimage.getall")]
        [CacheAspect(60)]
        [PerformanceAspect(1)]
        public IDataResult<List<CarImage>> GetAll() {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.ItemsListed);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId) {
            var images = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (images.Count > 0) {
                return new SuccessDataResult<List<CarImage>>(images);
            }
            images.Add(new CarImage { ImagePath = Paths.CarImagePath + "default.jpg" });
            return new ErrorDataResult<List<CarImage>>(images);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Update(IFormFile formFile, CarImage carImage) {
            var imageToUpdate = _carImageDal.Get(ci => ci.Id == carImage.Id); // Finding image
            if(imageToUpdate == null) {
                return new ErrorResult(Messages.ImageNotFound);
            }
            carImage.CarId = imageToUpdate.CarId;
            carImage.Date = DateTime.Now;
            carImage.ImagePath = imageToUpdate.ImagePath;

            _carImageDal.Update(carImage);

            FileHelper.Write(formFile, Paths.RootPath + imageToUpdate.ImagePath); // Overwriting file

            return new SuccessResult(Messages.ItemUpdated + carImage.Id);
        }



        private IResult CheckIfImageCountOfCarExceeded(int carId) {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count < 5) {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageCountExceeded);
        }


    }
}
