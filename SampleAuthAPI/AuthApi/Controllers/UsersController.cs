using System.Security.Claims;
using AuthApi.Entities;
using AuthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //POST http://localhost:4000/api/users/authenticate HTTP/1.1
        //Content-Type: application/json
        //username: test
        //password: test
        //cache-control: no-cache
        //Postman-Token: 73ff12c3-0de0-474c-9349-1c1b944cbea5
        //User-Agent: PostmanRuntime/7.6.0
        //Accept: */*
        //Host: localhost:4000
        //accept-encoding: gzip, deflate
        //content-length: 40
        //Connection: keep-alive
        //{"username": "test", "password": "test"}

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate2")]
        public IActionResult Authenticate()
        {
            
            var token = _userService.Authenticate();

            if (token == null)
                return BadRequest(new { message = "Unable to create auth token" });

            return Ok(token);
        }

        //GET http://localhost:4000/api/users HTTP/1.1
        //cache-control: no-cache
        //Postman-Token: 06cf7928-9bfb-48e2-9f14-b3a326421b7e
        //Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NzA3OTAyMjAsImV4cCI6MTU3MDc5MzgyMCwiaWF0IjoxNTcwNzkwMjIwfQ.mLIA4EybPhQIRt_601RDrqUH3RtZKAxuKj9050gkZf4
        //User-Agent: PostmanRuntime/7.6.0
        //Accept: */*
        //Host: localhost:4000
        //accept-encoding: gzip, deflate
        //Connection: keep-alive

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
