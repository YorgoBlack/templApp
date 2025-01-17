﻿using Microsoft.AspNetCore.Mvc;
using Templ.API.Middleware;
using Templ.Domain;
using Templ.Infrastucture.Services;

namespace Templ.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost(Name = "Signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SignIn([FromBody] SignInCommand cmd)
    {
        var user = _userService.ValidatePassword(cmd.UserName, cmd.Password);
        if (user != null)
        {
            return Ok(new {
                Token = _authService.Token(user.UserName),
                Message = "Success",
                UserName = user.SurName
            });
        }
        return new UnauthorizedResult();
    }

    [HttpPost(Name = "Signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SignUp([FromBody] AppUser newUser, string password)
    {
        var user = _userService.Create(newUser.UserName, password, newUser.FirstName, newUser.LastName);
        if (user != null)
        {
            return Ok(new
            {
                Token = _authService.Token(user.UserName),
                Message = "Success",
                UserName = user.SurName
            });
        }
        return StatusCode(StatusCodes.Status403Forbidden);
    }

    [HttpPost(Name = "Refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Refresh(string token) 
    {
        var (user,reToken) = _authService.RefreshToken(token);
        if( user != null && reToken != null) 
        {
            return Ok(new
            {
                Token = reToken,
                Message = "Success",
                UserName = user
            });
        }
        return StatusCode(StatusCodes.Status403Forbidden);
    }
}
