﻿using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Auth;
using CoffeeManagementAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        IStaffRepository _staffRepository;
        ITokenService _tokenService;
        IAuthorization _authorize;
        public AuthController(IStaffRepository staffRepository, ITokenService tokenService, IAuthorization authorization)
        {
            _staffRepository = staffRepository;
           _tokenService = tokenService;
            _authorize = authorization;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStaffDTO registerStaffDTO)
        {
            Staff newStaff = registerStaffDTO.toStaffFromRegister();
            var isSuccees =await _staffRepository.RegisterStaff(newStaff);
    
            if (!isSuccees)
            {
                return BadRequest(new ApiError("Username is existed"));
            }
            var accessToken = _tokenService.GenerateAccessToken(newStaff);
            var refreshToken = await _tokenService.GenerateRefreshToken(newStaff);
            return Ok(new
            {
                data= newStaff.toStaffDTO(),
                accessToken= accessToken,
                refreshToken=refreshToken,
            });
        }

        [HttpGet("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var authorHeader = Request.Headers["Authorization"].FirstOrDefault();
            if(authorHeader == null) { return Unauthorized(new ApiError("Token is missing")); }
            var token = authorHeader.Substring("Bearer ".Length).Trim();
            var accessToken = _tokenService.RefreshThisToken(token);
            
            return Ok(new
            {
                accessToken = accessToken,
            });
        }

        [HttpGet("check-token")]
        [Authorize]
        public IActionResult CheckIsToken()
        {
            return Ok(new
            {
                message = "Token is valid"
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginStaffDTO loginStaff)
        {
            Staff? staff =await _staffRepository.FindUser(loginStaff.Username);
            if(staff == null)
            {
                return Unauthorized(new ApiError("User is not found"));
            }
            bool checkPass = _authorize.VerifyPassword(staff, staff.Password, loginStaff.Password);
            if (!checkPass)
            {
                return Unauthorized("Password is incorrect");
            }
            var accessToken = _tokenService.GenerateAccessToken(staff);
            var refreshToken = await _tokenService.GenerateRefreshToken(staff);

            return Ok(new
            {
                data = staff.toStaffDTO(),
                accessToken = accessToken,
                refreshToken = refreshToken
            });
            ;

        }


    }
}
