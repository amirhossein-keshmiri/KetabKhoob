using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

public class BannerController : ApiController
{
    private readonly IBannerFacade _bannerFacade;


    public BannerController(IBannerFacade bannerFacade)
    {
        _bannerFacade = bannerFacade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<BannerDto>>> GetList()
    {
        var result = await _bannerFacade.GetBanners();
        return QueryResult(result);
    }

    [PermissionChecker(Permission.View_Banner)]
    [HttpGet("{id}")]
    public async Task<ApiResult<BannerDto?>> GetById(long id)
    {
        var result = await _bannerFacade.GetBannerById(id);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Create_Banner)]
    [HttpPost]
    public async Task<ApiResult> Create([FromForm] CreateBannerCommand command)
    {
        var result = await _bannerFacade.CreateBanner(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Edit_Banner)]
    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditBannerCommand command)
    {
        var result = await _bannerFacade.EditBanner(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Delete_Banner)]
    [HttpDelete("{bannerId}")]
    public async Task<ApiResult> Delete(long bannerId)
    {
        var result = await _bannerFacade.DeleteBanner(bannerId);
        return CommandResult(result);
    }
}

