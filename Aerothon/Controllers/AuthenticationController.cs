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
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public AuthenticationController(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var userResponse = _authenticationService.Login(loginRequest);
            return Ok(userResponse);
        }
    }
}
