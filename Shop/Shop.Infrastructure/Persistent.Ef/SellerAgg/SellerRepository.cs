using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;
internal class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    public SellerRepository(ShopContext context) : base(context)
    {
    }

    public Task<InventoryResult?> GetInventoryById(long id)
    {
        throw new NotImplementedException();
    }
}

