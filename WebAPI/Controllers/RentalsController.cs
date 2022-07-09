﻿using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase {
        IRentalService _rentalManager;

        public RentalsController(IRentalService rentalManager) {
            _rentalManager = rentalManager;
        }

        [HttpGet("get")]
        public IActionResult Get(int id) {
            var result = _rentalManager.Get(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            var result = _rentalManager.GetAll();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetails")]
        public IActionResult GetDetails() {
            var result = _rentalManager.GetDetails();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getoccupieddates")]
        public IActionResult GetEmptyDates(int carId) {
            var result = _rentalManager.GetOccupiedDates(carId);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(RentCarDto rentCarDto) {
            var result = _rentalManager.Add(rentCarDto.Rental, rentCarDto.CreditCard);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Rental rental) {
            var result = _rentalManager.Delete(rental);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Rental rental) {
            var result = _rentalManager.Update(rental);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
