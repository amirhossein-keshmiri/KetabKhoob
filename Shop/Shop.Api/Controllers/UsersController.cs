using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.SetRole;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class UsersController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IMapper _mapper;
    public UsersController(IUserFacade userFacade, IMapper mapper)
    {
        _userFacade = userFacade;
        _mapper = mapper;
    }

    [HttpGet]
    [PermissionChecker(Permission.View_User)]
    public async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userFacade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("Current")]
    public async Task<ApiResult<UserDto>> GetCurrentUser()
    {
        var result = await _userFacade.GetUserById(User.GetUserId());
        return QueryResult(result);
    }

    [HttpGet("{userId}")]
    [PermissionChecker(Permission.View_User)]
    public async Task<ApiResult<UserDto?>> GetById(long userId)
    {
        var result = await _userFacade.GetUserById(userId);
        return QueryResult(result);
    }

    [HttpPost]
    [PermissionChecker(Permission.Create_User)]
    public async Task<ApiResult> Create(CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        return CommandResult(result);
    }

    [HttpPut]
    [PermissionChecker(Permission.Edit_User)]
    public async Task<ApiResult> Edit([FromForm] EditUserCommand command)
    {
        var result = await _userFacade.EditUser(command);
        return CommandResult(result);
    }

    [HttpPut("Current")]
    public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
    {
        var commandModel = new EditUserCommand()
        {
            UserId = User.GetUserId(),
            Avatar = command.Avatar,
            Name = command.Name,
            Family = command.Family,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Gender = command.Gender
        };

        var result = await _userFacade.EditUser(commandModel);
        return CommandResult(result);
    }

    [HttpPut("ChangePassword")]
    public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
    {
        var changePasswordModel = _mapper.Map<ChangeUserPasswordCommand>(command);
        changePasswordModel.UserId = User.GetUserId();
        var result = await _userFacade.ChangePassword(changePasswordModel);
        return CommandResult(result);
    }

    [HttpPost("SetUserRole")]
    public async Task<ApiResult> SetRolesToUser(SetUserRoleCommand command)
    {
        var result = await _userFacade.SetUserRole(command);
        return CommandResult(result);
    }
}

