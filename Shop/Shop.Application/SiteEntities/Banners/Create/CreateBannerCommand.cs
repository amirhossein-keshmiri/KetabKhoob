using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommand : IBaseCommand
{
    public CreateBannerCommand(string link, IFormFile imageFile, BannerPosition position)
    {
        Link = link;
        ImageFile = imageFile;
        Position = position;
    }

    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}

