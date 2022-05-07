using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.MicroServices.FileManager;
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

        public IResult Add(IFormFile formFile, CarImage carImage) {
            var result = BusinessRules.Run(CheckIfImageCountOfCarExceeded(carImage.CarId));
            if(result != null) {
                return result;
            }

            string path = string.Format(@"{0}.jpg", Guid.NewGuid());
            carImage.ImagePath = Paths.CarImagePath + path;

            SetAndWriteImage(formFile, carImage, carImage.ImagePath);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ItemAdded + carImage.Id);
        }

        public IResult Delete(CarImage carImage) {
            string path = _carImageDal.Get(ci => ci.Id == carImage.Id).ImagePath; //Getting given carImage's path

            File.Delete(path); 
            _carImageDal.Delete(carImage);


            return new SuccessResult(Messages.ItemDeleted + carImage.Id);
        }

        public IDataResult<List<CarImage>> GetAll() {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.ItemsListed);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId) {
            var images = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (images.Count > 0) {
                var s = System.IO.Directory.GetCurrentDirectory();
                return new SuccessDataResult<List<CarImage>>();
            }
            images.Add(new CarImage { ImagePath = Paths.CarImagePath + "default.jpg" });
            return new ErrorDataResult<List<CarImage>>(images);
        }

        [TransactionScopeAspect]
        public IResult Update(IFormFile formFile, CarImage carImage) {
            var image = _carImageDal.Get(ci => ci.Id == carImage.Id); // Finding image
            carImage.CarId = image.CarId; 

            SetAndWriteImage(formFile, carImage, image.ImagePath); // Overwriting file
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ItemUpdated + carImage.Id);
        }




        private IResult CheckIfImageCountOfCarExceeded(int carId) {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count < 5) {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageExceeded);
        }

        private void SetAndWriteImage(IFormFile formFile, CarImage carImage, string path) {
            carImage.Date = DateTime.Now;
            carImage.ImagePath = path;
            FileManager.Create(formFile, carImage.ImagePath);
        }

    }
}
