using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
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
    public class UsersController : ControllerBase {
        IUserService _userManager;
        public UsersController(IUserService userManager) {
            _userManager = userManager;
        }

        [HttpGet("getuserclaims")]
        public IActionResult GetUserClaims(int userId) {
            var result = _userManager.GetUserClaims(userId);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getoperationclaims")]
        public IActionResult GetOperationClaims() {
            var result = _userManager.GetOperationClaims();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addclaim")]
        public IActionResult AddClaim(UserOperationClaim operationClaim) {
            var result = _userManager.AddClaim(operationClaim);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteclaim")]
        public IActionResult DeleteClaim(UserOperationClaim operationClaim) {
            var result = _userManager.DeleteClaim(operationClaim);
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

        [HttpPost("changepassword")]
        public IActionResult ChangePassword(ChangePasswordDto user) {
            var result = _userManager.ChangePassword(user);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UserDto user) {
            var result = _userManager.Update(user);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int id) {
            var result = _userManager.GetUser(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getusers")]
        public IActionResult GetUsers() {
            var result = _userManager.GetUsers();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbymail")]
        public IActionResult GetByMail(string email) {
            var result = _userManager.GetByMail(email);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
