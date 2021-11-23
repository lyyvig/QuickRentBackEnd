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
    public class ColorsController : ControllerBase {
        IColorService _colorManager;

        public ColorsController(IColorService colorManager) {
            _colorManager = colorManager;
        }

        [HttpGet("get")]
        public IActionResult Get(int id) {
            var result = _colorManager.Get(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            var result = _colorManager.GetAll();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Color color) {
            var result = _colorManager.Add(color);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Color color) {
            var result = _colorManager.Delete(color);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Color color) {
            var result = _colorManager.Update(color);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
