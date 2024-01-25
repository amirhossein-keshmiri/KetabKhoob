using Shop.Domain.SiteEntities.Repositories;
using Shop.Domain.SiteEntities;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;
internal class ShippingMethodRepository : BaseRepository<ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(ShippingMethod method)
    {
        Context.ShippingMethods.Remove(method);
    }
}

