using AutoMapper;
using BudgetManagement.Core.Entities;
using BudgetManagement.Core.Helpers;
using BudgetManagement.Server.Dtos.Auth;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Server.Controllers;

[ApiController]
[Route("auth")]
[AllowAnonymous]
public class AuthController(
    IUserService userService,
    IAuthService authService,
    IMapper mapper,
    ILogger<AuthController> logger)
    : ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInDto signInDto, CancellationToken cancellationToken)
    {
        var foundUser = await userService.GetByLoginAsync(signInDto.Login, cancellationToken);
        if (foundUser is null)
        {
            return Unauthorized("It is impossible to sign in with the passed email.");
        }

        var actualHash = signInDto.Password.GetPasswordHash();
        var expectedHash = foundUser.PasswordHash;

        if (!expectedHash.Equals(actualHash))
        {
            return Unauthorized("Incorrect login or password.");
        }

        var accessToken = await authService.GetUserTokenAsync(foundUser!);

        return Ok(new AccessTokenDto(accessToken));
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpDto signUpDto, CancellationToken cancellationToken)
    {
        var foundUser = await userService.GetByLoginAsync(signUpDto.Login, cancellationToken);
        if (foundUser is not null)
        {
            return BadRequest("An account with the specified email address already exists.");
        }

        if (!signUpDto.Password.Equals(signUpDto.ConfirmPassword))
        {
            return BadRequest("The password is not equal to the confirmation password.");
        }

        var userToCreate = mapper.Map<User>(signUpDto);
        userToCreate.PasswordHash = signUpDto.Password.GetPasswordHash();
        userToCreate.Id = Guid.NewGuid();

        var createdUser = await userService.CreateAsync(userToCreate, cancellationToken);
        var accessToken = await authService.GetUserTokenAsync(createdUser);

        return Ok(new AccessTokenDto(accessToken));
    }
}
