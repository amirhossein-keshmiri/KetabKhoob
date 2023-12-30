using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.EditAddress;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users.DTOs;
using Shop.Application.Users.DeleteAddress;
using Microsoft.AspNetCore.Authorization;
using Common.Domain.ValueObjects;

namespace Shop.Api.Controllers;

[Authorize]
public class UserAddressController : ApiController
{
    private readonly IUserAddressFacade _userAddress;
    private readonly IMapper _mapper;
    public UserAddressController(IUserAddressFacade userAddress, IMapper mapper)
    {
        _userAddress = userAddress;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ApiResult<List<AddressDto>>> GetList()
    {
        var result = await _userAddress.GetList(User.GetUserId());
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<AddressDto?>> GetById(long id)
    {
        var result = await _userAddress.GetById(id);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> AddAddress(AddUserAddressViewModel viewModel)
    {
        //var command = _mapper.Map<AddUserAddress
        //Command>(viewModel);
        //command.UserId = User.GetUserId();

        var command = new AddUserAddressCommand(User.GetUserId(), viewModel.State, viewModel.City, viewModel.PostalCode,
           viewModel.PostalAddress, new PhoneNumber(viewModel.PhoneNumber), viewModel.Name,
           viewModel.Family, viewModel.NationalCode);

        var result = await _userAddress.AddAddress(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditAddress(EditUserAddressViewModel viewModel)
    {
        //var command = _mapper.Map<EditUserAddressCommand>(viewModel);

        var command = new EditUserAddressCommand(User.GetUserId(), viewModel.Id, viewModel.State,
            viewModel.City, viewModel.PostalCode, viewModel.PostalAddress, 
            new PhoneNumber(viewModel.PhoneNumber), viewModel.Name,
           viewModel.Family, viewModel.NationalCode);

        command.UserId = User.GetUserId();
        var result = await _userAddress.EditAddress(command);
        return CommandResult(result);
    }

    [HttpDelete("{addressId}")]
    public async Task<ApiResult> DeleteAddress(long addressId)
    {
        var result = await _userAddress.DeleteAddress(new DeleteUserAddressCommand(User.GetUserId(), addressId));
        return CommandResult(result);
    }

}

