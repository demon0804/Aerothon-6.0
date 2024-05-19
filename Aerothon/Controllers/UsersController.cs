using Aerothon.Models.Request;
using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aerothon.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [Route("/[controller]")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Signups the specified user request.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <returns></returns>
        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] UserRequest userRequest)
        {
            var userId = _userService.Add(userRequest);
            return Ok(userId);
        }
    }
}
