using AutoMapper;
using BuyAndSell.Business.Services.Contracts;
using BuyAndSell.Contracts.DTOs.Auth;
using BuyAndSell.Contracts.DTOs.User;
using BuyAndSell.Contracts.Exceptions;
using BuyAndSell.Data.Enums;
using BuyAndSell.Data.Resources;
using BuySell.Host.Extensions;
using BuySell.Host.Validators.User;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuySell.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "Active")]
        public async Task<IActionResult> GetAll([FromQuery] UserQuery query)
        {
            var response = await _userService.GetAllUsersAsync(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Active")]
        public async Task<IActionResult> GetUserById(long id, [FromQuery] Query query)
        {
            var user = await _userService.GetUserByIdAsync(id, query);

            if (user is null)
                return NotFound("Korisnik nije pronađen");

            var response = _mapper.Map<UserViewDto>(user);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        {
            var result = await new CreateUserDtoValidator().ValidateAsync(dto);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var userResponse = await _userService.CreateUserAsync(dto);

            return Ok();
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "Active")]
        public async Task<IActionResult> UpdateUser(long id,[FromBody] UserUpdateDto dto)
        {
            if (id != User.GetUserId()) throw new MethodNotAllowedException("Nije moguce menjati informacije drugih korisnika");

            var result = await new UpdateUserDtoValidator().ValidateAsync(dto);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            

            await _userService.UpdateUserAsync(dto, id);

            return Ok(dto);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "Active")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto requestDto, long id)
        {
            var result = await new ChangePasswordUserDtoValidator().ValidateAsync(requestDto);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            var response = await _userService.ChangePasswordAsync(requestDto, id);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDto model)
        {
            var response = await _userService.AuthenticateAsync(model);

            return response is not null ? Ok(response) : BadRequest(new { Message = "Pogrešan email ili lozinka" });
        }

        [AllowAnonymous]
        [HttpPatch("authenticate")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto model)
        {
            var response = await _userService.RefreshTokenAsync(model.RefreshToken);

            return Ok(response);
        }

        [HttpPut("authenticate/logout")]
        public async Task<IActionResult> LogOut([FromBody] RefreshTokenDto model)
        {
            var loggedUserId = User.GetUserId();
            await _userService.RevokeTokenAsync(loggedUserId, model.RefreshToken);
            return Ok();
        }
    }
}
