using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase {
        ICarImageService _carImageManager;

        public CarImagesController(ICarImageService carImageManager) {
            _carImageManager = carImageManager;
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            return Ok(_carImageManager.GetAll());
        }

        [HttpGet("listbycarid")]
        public IActionResult ListByCarId(int carId) {
            var images = _carImageManager.GetByCarId(carId);
            if (images.Success) {
                return Ok(images);
            }
            return BadRequest(images);
        }


        [HttpPost("add")]
        public IActionResult Add(IFormFile file, [FromForm] CarImage carImage) {
            var result = _carImageManager.Add(file, carImage);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(IFormFile file, [FromForm] CarImage carImage) {
            var result = _carImageManager.Update(file, carImage);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImage) {
            var result = _carImageManager.Delete(carImage);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }



    }
}
