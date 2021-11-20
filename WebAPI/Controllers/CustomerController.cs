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
    public class CustomerController : ControllerBase {
        ICustomerService _customerManager;

        public CustomerController(ICustomerService customerManager) {
            _customerManager = customerManager;
        }

        [HttpGet("get")]
        public IActionResult GetAll(int id) {
            var result = _customerManager.Get(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            var result = _customerManager.GetAll();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Customer customer) {
            var result = _customerManager.Add(customer);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer customer) {
            var result = _customerManager.Delete(customer);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Customer customer) {
            var result = _customerManager.Update(customer);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
