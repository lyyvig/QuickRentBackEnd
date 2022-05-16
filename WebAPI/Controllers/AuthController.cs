﻿using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller {
        private IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto) {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success) {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success) {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto) {
            

            var registerResult = _authService.Register(userForRegisterDto);

            if(!registerResult.Success) {
                return BadRequest(registerResult.Message);
            }
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success) {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
