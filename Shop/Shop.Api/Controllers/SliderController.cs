using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Slider;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

public class SliderController : ApiController
{
    private readonly ISliderFacade _sliderFacade;


    public SliderController(ISliderFacade sliderFacade)
    {
        _sliderFacade = sliderFacade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<SliderDto>>> GetList()
    {
        var result = await _sliderFacade.GetSliders();
        return QueryResult(result);
    }

    [PermissionChecker(Permission.View_Slider)]
    [HttpGet("{id}")]
    public async Task<ApiResult<SliderDto?>> GetById(long id)
    {
        var result = await _sliderFacade.GetSliderById(id);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Create_Slider)]
    [HttpPost]
    public async Task<ApiResult> Create([FromForm] CreateSliderCommand command)
    {
        var result = await _sliderFacade.CreateSlider(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Edit_Slider)]
    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditSliderCommand command)
    {
        var result = await _sliderFacade.EditSlider(command);
        return CommandResult(result);
    }
}

