using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Octapull.Application.Abstractions;
using Octapull.Application.Abstractions.Storage;
using Octapull.Application.Dtos.Account.Request;
using Octapull.Application.Dtos.Account.Response;
using Octapull.Application.Dtos.Account.User;
using Octapull.Application.Interfaces;
using Octapull.Domain.Identity;
using System.Diagnostics.Eventing.Reader;

namespace Octapull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IIdentityService _identityService;
        private readonly IBlobService _blobService;
        private readonly IMapper _mapper;

        public AuthController(ITokenService tokenService, IIdentityService userService, IBlobService blobService, IMapper mapper)
        {
            _tokenService = tokenService;
            _identityService = userService;
            _blobService = blobService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromForm] LoginRequestDto loginRequestDto)
        {
            var user = await _identityService.GetUserByEmailAsync(loginRequestDto.Email);
            if (user == null) return Unauthorized("Invalid email");


            var result = await _identityService.SignInUser(user, loginRequestDto.Password);
            if (!result) return Unauthorized("Invalid password");

            return new LoginResponseDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromForm] RegisterRequestDto registerDto)
        {
            var isUserNull = await _identityService.GetUserByEmailAsync(registerDto.Email);

            if (isUserNull != null)
            {
                return BadRequest("User already exists");
            }

            var file = registerDto.Image;  

            var fileId = await _blobService.UploadAsync(file.OpenReadStream(), "images", file.ContentType);

            var createUserRequest = _mapper.Map<CreateUserRequestDto>(registerDto);

            createUserRequest.ImageId = fileId.ToString();

            var (result, userId) = await _identityService.CreateUserAsync(createUserRequest);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            //// Generate email confirmation token
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //// Construct confirmation link
            //var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token }, Request.Scheme);

            //// Send confirmation email
            //await _emailService.SendConfirmationEmailAsync(model.Email, confirmationLink);

            //return Ok("User registered successfully. Please check your email for confirmation.");

            var user = await _identityService.GetUserByIdAsync(userId);

            var token = _tokenService.CreateToken(user);

            return new RegisterResponseDto
            {
                Email = user.Email,
                Token = token
            };

        }
    }
}
