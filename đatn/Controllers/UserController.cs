﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using đatn.Models;
using đatn.Service;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Endpoint đăng ký người dùng
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (model == null)
        {
            return BadRequest("Dữ liệu người dùng không hợp lệ.");
        }

        await _userService.RegisterUserAsync(model);
        return Ok("Đăng ký thành công.");
    }

    // Endpoint đăng nhập người dùng
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Dữ liệu đăng nhập không hợp lệ.");
        }

        var token = await _userService.LoginAsync(model);
        if (token == null)
        {
            return Unauthorized("Thông tin đăng nhập không chính xác.");
        }

        return Ok(new
        {
            message = "Đăng nhập thành công.",
            token = token
        });
    }

    // Endpoint lấy thông tin người dùng
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        var user = await _userService.GetUserAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // Endpoint cập nhật thông tin người dùng
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] User user)
    {
        var existingUser = await _userService.GetUserAsync(userId);
        if (existingUser == null)
        {
            return NotFound();
        }

        await _userService.UpdateUserAsync(userId, user);
        return NoContent();
    }

    // Endpoint xóa tài khoản người dùng
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var existingUser = await _userService.GetUserAsync(userId);
        if (existingUser == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(userId);
        return NoContent();
    }
}
