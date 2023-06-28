using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.DTOs.Authentication;
using SalesSystem.Services.Concretes;

namespace SalesSystem.Api.Controllers;
[ApiController]
public class AuthenticationController : ControllerBase
{
    public readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("auth/register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto dto)
    {
        await _userService.RegisterAsync(dto);
        return Ok();
    }
    [HttpPost("auth/login")]
    public async Task<IActionResult> LoginAsync([FromForm] LoginDto dto)
    {
        return Ok(await _userService.SignInAsync(dto.Email, dto.Password));
    }

}
