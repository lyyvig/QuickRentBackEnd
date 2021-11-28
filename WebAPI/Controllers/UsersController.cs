using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        IUserService _userManager;

        public UsersController(IUserService userManager) {
            _userManager = userManager;
        }

        [HttpGet("getclaims")]
        public IActionResult GetClaims(User user) {
            var result = _userManager.GetClaims(user);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(User user) {
            var result = _userManager.Add(user);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbymail")]
        public IActionResult GetByMail(User user) {
            var result = _userManager.GetByMail(user.Email);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
