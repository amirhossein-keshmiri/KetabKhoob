using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
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

    [HttpGet]
    public async Task<ApiResult<List<SliderDto>>> GetList()
    {
        var result = await _sliderFacade.GetSliders();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<SliderDto?>> GetById(long id)
    {
        var result = await _sliderFacade.GetSliderById(id);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> Create([FromForm] CreateSliderCommand command)
    {
        var result = await _sliderFacade.CreateSlider(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditSliderCommand command)
    {
        var result = await _sliderFacade.EditSlider(command);
        return CommandResult(result);
    }
}

