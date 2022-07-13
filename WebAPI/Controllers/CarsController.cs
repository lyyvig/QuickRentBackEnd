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
    public class CarsController : ControllerBase {
        ICarService _carManager;

        public CarsController(ICarService carManager) {
            _carManager = carManager;
        }

        [HttpGet("get")]
        public IActionResult Get(int id) {
            var result = _carManager.Get(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarstats")]
        public IActionResult GetCarStats(int id) {
            var result = _carManager.GetCarStats(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcardetail")]
        public IActionResult GetCarDetail(int carId) {
            var result = _carManager.GetCarDetail(carId);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetails")]
        public IActionResult GetDetails() {
            var result = _carManager.GetDetails();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getdetailsbyfilter")]
        public IActionResult GetDetailsByFilter(FilterOptions filter) {
            var result = _carManager.GetDetailsByFilter(filter);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            var result = _carManager.GetAll();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Car car) {
            var result = _carManager.Add(car);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Car car) {
            var result = _carManager.Delete(car);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Car car) {
            var result = _carManager.Update(car);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
