﻿using Shop.Domain.SiteEntities.Repositories;
using Shop.Domain.SiteEntities;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;
internal class SliderRepository : BaseRepository<Slider>, ISliderRepository
{
    public SliderRepository(ShopContext context) : base(context)
    {
    }
}
